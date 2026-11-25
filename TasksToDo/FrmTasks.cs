using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TasksToDo
{
    public partial class FrmTasks : Form
    {
        private List<Tarefa> lstTarefas;
        private Tarefa tarefaEdicao;
        private bool funcionariosAlterados;
        private bool alterado;

        public FrmTasks()
        {
            InitializeComponent();
            lstTarefas = new List<Tarefa>();
            tarefaEdicao = null;
            funcionariosAlterados = false;
            alterado = false;
        }

        private void FrmTasks_Load(object sender, EventArgs e)
        {
            Ficheiros.RegistaInicio();

            try
            {
                foreach (string nome in Ficheiros.LerNomes(Ficheiros.FichFuncionarios))
                    cbbFuncionario.Items.Add(nome);
                foreach (string nome in Ficheiros.LerNomes(Ficheiros.FichEquipas))
                    cbbEquipa.Items.Add(nome);
            }
            catch (FileNotFoundException ex)
            {
                MostraErro("Não foi encontrado o ficheiro:\n" + ex.FileName +
                    "\n\nA aplicação precisa dos ficheiros funcionarios.txt e equipas.txt " +
                    "na pasta:\n" + Ficheiros.Pasta + "\n\nA aplicação vai terminar.", ex);
                Close();
                return;
            }
            catch (DirectoryNotFoundException ex)
            {
                MostraErro("Não existe a pasta de dados:\n" + Ficheiros.Pasta +
                    "\n\nCrie a pasta e coloque lá os ficheiros funcionarios.txt e equipas.txt." +
                    "\n\nA aplicação vai terminar.", ex);
                Close();
                return;
            }
            catch (IOException ex)
            {
                MostraErro("Não foi possível ler os ficheiros de funcionários e equipas " +
                    "(podem estar abertos noutra aplicação).\n\nA aplicação vai terminar.", ex);
                Close();
                return;
            }
            catch (Exception ex)
            {
                MostraErro("Erro inesperado ao ler os ficheiros iniciais.\n\nA aplicação vai terminar.", ex);
                Close();
                return;
            }

            try
            {
                int invalidas;
                lstTarefas = Ficheiros.LerTarefas(out invalidas);
                if (invalidas > 0)
                {
                    MessageBox.Show(invalidas + " linha(s) do ficheiro de tarefas tinham erros e " +
                        "foram ignoradas.\nO detalhe está no ficheiro:\n" + Ficheiros.FichLog,
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (IOException ex)
            {
                MostraErro("Não foi possível ler o ficheiro de tarefas.\n" +
                    "A aplicação vai iniciar sem tarefas.", ex);
                lstTarefas = new List<Tarefa>();
            }
            catch (Exception ex)
            {
                MostraErro("Erro inesperado ao ler o ficheiro de tarefas.\n" +
                    "A aplicação vai iniciar sem tarefas.", ex);
                lstTarefas = new List<Tarefa>();
            }

            foreach (Tarefa t in lstTarefas)
            {
                if (cbbEquipa.FindStringExact(t.Equipa) == -1)
                    cbbEquipa.Items.Add(t.Equipa);
                if (cbbFuncionario.FindStringExact(t.Funcionario) == -1)
                {
                    cbbFuncionario.Items.Add(t.Funcionario);
                    funcionariosAlterados = true;
                }
            }

            PreencheTreeView();
        }

        private void PreencheTreeView()
        {
            tvwEquipas.BeginUpdate();
            tvwEquipas.Nodes.Clear();

            foreach (Tarefa t in lstTarefas.OrderBy(x => x.Equipa)
                                           .ThenBy(x => x.Funcionario)
                                           .ThenBy(x => x.DataInicio))
            {
                TreeNode nodeEquipa = ProcuraNo(tvwEquipas.Nodes, t.Equipa);
                if (nodeEquipa == null)
                {
                    nodeEquipa = new TreeNode();
                    nodeEquipa.Tag = t.Equipa;
                    nodeEquipa.Text = t.Equipa;
                    tvwEquipas.Nodes.Add(nodeEquipa);
                }

                TreeNode nodeFunc = ProcuraNo(nodeEquipa.Nodes, t.Funcionario);
                if (nodeFunc == null)
                {
                    nodeFunc = new TreeNode();
                    nodeFunc.Tag = t.Funcionario;
                    nodeFunc.Text = t.Funcionario;
                    if (t.Coordenador)
                        nodeFunc.Text += " (Coordenador)";
                    nodeEquipa.Nodes.Add(nodeFunc);
                }

                TreeNode nodeTarefa = new TreeNode();
                nodeTarefa.Tag = t;
                nodeTarefa.Text = t.Nome;
                if (t.Responsavel)
                    nodeTarefa.Text += " [Responsável]";
                nodeFunc.Nodes.Add(nodeTarefa);
            }

            tvwEquipas.ExpandAll();
            tvwEquipas.EndUpdate();
        }

        private TreeNode ProcuraNo(TreeNodeCollection nodes, string chave)
        {
            foreach (TreeNode tn in nodes)
            {
                if (tn.Tag.ToString() == chave)
                    return tn;
            }
            return null;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            DateTime inicio, fim;

            bool valFunc = ValidFuncionario();
            bool valEquipa = ValidEquipa();
            bool valTarefa = ValidTarefa();
            bool valDesc = ValidDescricao();
            bool valDatas = ValidDatas(out inicio, out fim);
            if (!(valFunc && valEquipa && valTarefa && valDesc && valDatas))
            {
                MostraErro("Existem dados por preencher ou com erros.\n" +
                    "Passe o rato sobre os ícones vermelhos para ver o que tem de corrigir.", null);
                return;
            }

            string func = cbbFuncionario.Text.Trim();
            int idxFunc = cbbFuncionario.FindStringExact(func);
            if (idxFunc != -1)
                func = cbbFuncionario.Items[idxFunc].ToString();
            string equipa = cbbEquipa.SelectedItem.ToString();
            string nome = txtTarefa.Text.Trim();

            List<Tarefa> outras = lstTarefas.Where(x => x != tarefaEdicao).ToList();
            if (ValidRegras(outras, func, equipa, nome, inicio, fim) == false)
                return;

            if (idxFunc == -1)
            {
                cbbFuncionario.Items.Add(func);
                funcionariosAlterados = true;
            }

            if (tarefaEdicao != null)
                lstTarefas.Remove(tarefaEdicao);

            bool coord = chkCoordenador.Checked;
            if (coord == false && tarefaEdicao == null)
                coord = outras.Any(x => x.Equipa == equipa && x.Funcionario == func && x.Coordenador);
            foreach (Tarefa tf in lstTarefas.Where(x => x.Equipa == equipa && x.Funcionario == func))
                tf.Coordenador = coord;

            Tarefa t = new Tarefa();
            t.Equipa = equipa;
            t.Funcionario = func;
            t.Coordenador = coord;
            t.Nome = nome;
            t.Responsavel = chkResponsavel.Checked;
            t.DataInicio = inicio;
            t.DataFim = fim;
            t.Descricao = txtDescricao.Text.Trim();
            lstTarefas.Add(t);

            alterado = true;
            PreencheTreeView();
            LimpaControlos();
        }

        private bool ValidRegras(List<Tarefa> outras, string func, string equipa,
                                 string nome, DateTime inicio, DateTime fim)
        {
            if (outras.Any(x => x.Funcionario == func && MesmoNome(x.Nome, nome)))
            {
                erpUI.SetError(txtTarefa, "Tarefa já atribuída a este funcionário.");
                MostraErro("A tarefa \"" + nome + "\" já está atribuída a " + func + ".", null);
                return false;
            }

            List<string> funcsEquipa = outras.Where(x => x.Equipa == equipa)
                                             .Select(x => x.Funcionario)
                                             .Distinct().ToList();
            if (funcsEquipa.Contains(func) == false && funcsEquipa.Count >= 5)
            {
                erpUI.SetError(cbbEquipa, "Equipa completa.");
                MostraErro("A equipa " + equipa + " já tem 5 " +
                    "funcionários (máximo permitido).", null);
                return false;
            }

            int simultaneas = outras.Count(x => x.Funcionario == func &&
                                                x.DataInicio <= fim && x.DataFim >= inicio);
            if (simultaneas >= 3)
            {
                erpUI.SetError(cbbFuncionario, "Demasiadas tarefas em simultâneo.");
                MostraErro(func + " já tem " + simultaneas + " tarefas entre " +
                    inicio.ToString("dd/MM/yyyy") + " e " + fim.ToString("dd/MM/yyyy") +
                    ".\nCada funcionário só pode ter 3 " +
                    "tarefas em simultâneo.", null);
                return false;
            }

            if (chkResponsavel.Checked)
            {
                Tarefa resp = outras.FirstOrDefault(x => MesmoNome(x.Nome, nome) && x.Responsavel);
                if (resp != null)
                {
                    erpUI.SetError(chkResponsavel, "A tarefa já tem responsável.");
                    MostraErro("A tarefa \"" + nome + "\" já tem como responsável " +
                        resp.Funcionario + " (equipa " + resp.Equipa + ").", null);
                    return false;
                }
            }

            if (chkCoordenador.Checked)
            {
                Tarefa coord = outras.FirstOrDefault(x => x.Equipa == equipa &&
                                                          x.Coordenador && x.Funcionario != func);
                if (coord != null)
                {
                    erpUI.SetError(chkCoordenador, "A equipa já tem coordenador.");
                    MostraErro("A equipa " + equipa + " já tem como coordenador " +
                        coord.Funcionario + ".", null);
                    return false;
                }
            }

            return true;
        }

        private bool MesmoNome(string a, string b)
        {
            return a.Trim().ToLower() == b.Trim().ToLower();
        }

        private bool ValidFuncionario()
        {
            if (String.IsNullOrEmpty(cbbFuncionario.Text.Trim()))
            {
                erpUI.SetError(cbbFuncionario, "Escreva ou seleccione o nome do funcionário.");
                return false;
            }
            if (cbbFuncionario.Text.Contains(";"))
            {
                erpUI.SetError(cbbFuncionario, "O nome não pode conter o carácter ';'.");
                return false;
            }
            erpUI.SetError(cbbFuncionario, null);
            return true;
        }

        private bool ValidEquipa()
        {
            if (cbbEquipa.SelectedIndex == -1)
            {
                erpUI.SetError(cbbEquipa, "Seleccione uma equipa.");
                return false;
            }
            erpUI.SetError(cbbEquipa, null);
            return true;
        }

        private bool ValidTarefa()
        {
            if (String.IsNullOrEmpty(txtTarefa.Text.Trim()))
            {
                erpUI.SetError(txtTarefa, "Escreva o nome da tarefa.");
                return false;
            }
            if (txtTarefa.Text.Contains(";"))
            {
                erpUI.SetError(txtTarefa, "O nome da tarefa não pode conter o carácter ';'.");
                return false;
            }
            erpUI.SetError(txtTarefa, null);
            erpUI.SetError(chkResponsavel, null);
            erpUI.SetError(chkCoordenador, null);
            return true;
        }

        private bool ValidDescricao()
        {
            if (txtDescricao.Text.Contains(";"))
            {
                erpUI.SetError(txtDescricao, "A descrição não pode conter o carácter ';'.");
                return false;
            }
            erpUI.SetError(txtDescricao, null);
            return true;
        }

        private bool ValidDatas(out DateTime inicio, out DateTime fim)
        {
            bool okInicio = LerData(mtxtInicio, out inicio);
            bool okFim = LerData(mtxtFim, out fim);
            if (okInicio && okFim && fim < inicio)
            {
                erpUI.SetError(mtxtFim, "A data de fim não pode ser anterior à data de início (" +
                    inicio.ToString("dd/MM/yyyy") + ").");
                return false;
            }
            return okInicio && okFim;
        }

        private bool LerData(MaskedTextBox mtxt, out DateTime data)
        {
            data = DateTime.MinValue;
            if (!mtxt.MaskFull)
            {
                erpUI.SetError(mtxt, "Preencha a data (dd/mm/aaaa).");
                return false;
            }
            try
            {
                data = DateTime.ParseExact(mtxt.Text, "dd/MM/yyyy",
                    CultureInfo.CurrentCulture);
                erpUI.SetError(mtxt, null);
                return true;
            }
            catch (FormatException)
            {
                erpUI.SetError(mtxt, "A data " + mtxt.Text + " não existe.");
                return false;
            }
        }

        private void Texto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ';')
            {
                e.Handled = true;
                erpUI.SetError((Control)sender, "O carácter ';' não é permitido.");
            }
        }

        private void LimpaControlos()
        {
            cbbFuncionario.Text = "";
            cbbEquipa.SelectedIndex = -1;
            txtTarefa.Text = "";
            chkCoordenador.Checked = false;
            chkResponsavel.Checked = false;
            mtxtInicio.Text = "";
            mtxtFim.Text = "";
            txtDescricao.Text = "";
            erpUI.Clear();
            tarefaEdicao = null;
            this.Text = "Tasks To Do";
            cbbFuncionario.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Guardar())
            {
                MessageBox.Show("Dados guardados na pasta:\n" + Ficheiros.Pasta, this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool Guardar()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Ficheiros.EscreverTarefas(lstTarefas);

                if (funcionariosAlterados)
                {
                    List<string> nomes = new List<string>();
                    foreach (object item in cbbFuncionario.Items)
                        nomes.Add(item.ToString());
                    Ficheiros.EscreverNomes(Ficheiros.FichFuncionarios, nomes);
                    funcionariosAlterados = false;
                }

                alterado = false;
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                MostraErro("Não tem permissões para escrever na pasta:\n" + Ficheiros.Pasta, ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                MostraErro("A pasta de dados já não existe:\n" + Ficheiros.Pasta, ex);
            }
            catch (IOException ex)
            {
                MostraErro("Não foi possível escrever os ficheiros.\n" +
                    "Verifique se não estão abertos noutra aplicação.", ex);
            }
            catch (Exception ex)
            {
                MostraErro("Erro inesperado ao guardar os dados.", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return false;
        }

        private void FrmTasks_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (alterado == false)
                return;

            DialogResult res = MessageBox.Show("Existem alterações que não foram guardadas.\n" +
                "Quer guardar antes de sair?", this.Text, MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (res == DialogResult.Cancel)
                e.Cancel = true;
            else if (res == DialogResult.Yes && Guardar() == false)
                e.Cancel = true;
        }

        private void cmsTree_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TreeNode node = tvwEquipas.GetNodeAt(tvwEquipas.PointToClient(Cursor.Position));
            if (node == null)
            {
                e.Cancel = true;
                return;
            }
            tvwEquipas.SelectedNode = node;

            tsmiEditar.Enabled = node.Level == 2;
            tsmiApagar.Enabled = node.Level == 2;
            tsmiDetalhes.Enabled = node.Level == 2;
            tsmiActivas.Enabled = node.Level == 1;
            tsmiLista.Enabled = node.Level == 1;
            tsmiCarga.Enabled = node.Level == 0;
            tsmiDesempenho.Enabled = node.Level == 0;
        }

        private void tsmiEditar_Click(object sender, EventArgs e)
        {
            Tarefa t = (Tarefa)tvwEquipas.SelectedNode.Tag;

            cbbFuncionario.Text = t.Funcionario;
            cbbEquipa.SelectedItem = t.Equipa;
            txtTarefa.Text = t.Nome;
            chkCoordenador.Checked = t.Coordenador;
            chkResponsavel.Checked = t.Responsavel;
            mtxtInicio.Text = t.DataInicio.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
            mtxtFim.Text = t.DataFim.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
            txtDescricao.Text = t.Descricao;
            erpUI.Clear();

            tarefaEdicao = t;
            this.Text = "Tasks To Do - a editar \"" + t.Nome + "\" de " + t.Funcionario +
                " (Adicionar para gravar a alteração)";
            txtTarefa.Focus();
        }

        private void tsmiApagar_Click(object sender, EventArgs e)
        {
            Tarefa t = (Tarefa)tvwEquipas.SelectedNode.Tag;

            DialogResult res = MessageBox.Show("Desatribuir a tarefa \"" + t.Nome + "\" de " +
                t.Funcionario + " (equipa " + t.Equipa + ")?", this.Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.No)
                return;

            lstTarefas.Remove(t);
            if (tarefaEdicao == t)
                LimpaControlos();
            alterado = true;
            PreencheTreeView();
        }

        private void tsmiDetalhes_Click(object sender, EventArgs e)
        {
            Tarefa t = (Tarefa)tvwEquipas.SelectedNode.Tag;
            List<Tarefa> atribuicoes = lstTarefas.Where(x => MesmoNome(x.Nome, t.Nome))
                                                 .OrderBy(x => x.Funcionario).ToList();

            string str = "Tarefa: " + t.Nome + "\n";
            str += "Período: " + t.DataInicio.ToString("dd/MM/yyyy") + " a " +
                   t.DataFim.ToString("dd/MM/yyyy") + " (" + t.Estado() + ")\n";
            str += "Descrição: " + (t.Descricao == "" ? "(sem descrição)" : t.Descricao) + "\n\n";
            str += "Funcionários com esta tarefa (" + atribuicoes.Count + "):\n";
            foreach (Tarefa a in atribuicoes)
            {
                str += "  • " + a.Funcionario + " - equipa " + a.Equipa;
                if (a.Responsavel)
                    str += " - RESPONSÁVEL";
                if (a.Coordenador)
                    str += " - coordenador da equipa";
                str += "\n";
            }
            MessageBox.Show(str, "Detalhes da tarefa", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void tsmiActivas_Click(object sender, EventArgs e)
        {
            MostraTarefasFuncionario(tvwEquipas.SelectedNode.Tag.ToString(), true);
        }

        private void tsmiLista_Click(object sender, EventArgs e)
        {
            MostraTarefasFuncionario(tvwEquipas.SelectedNode.Tag.ToString(), false);
        }

        private void MostraTarefasFuncionario(string func, bool soActivas)
        {
            List<Tarefa> tarefas = lstTarefas.Where(x => x.Funcionario == func &&
                                                         (soActivas == false || x.Activa()))
                                             .OrderBy(x => x.DataInicio).ToList();

            string titulo = soActivas ? "Tarefas activas" : "Lista de tarefas";
            string str = titulo + " de " + func + " (" + tarefas.Count + "):\n\n";
            if (tarefas.Count == 0)
                str += soActivas ? "Não está a desenvolver nenhuma tarefa hoje." : "Sem tarefas.";
            foreach (Tarefa t in tarefas)
            {
                str += "  • " + t.Nome + " (equipa " + t.Equipa + ")\n";
                str += "     de " + t.DataInicio.ToString("dd/MM/yyyy") + " a " +
                       t.DataFim.ToString("dd/MM/yyyy");
                if (soActivas == false)
                    str += " - " + t.Estado();
                str += "\n";
            }
            MessageBox.Show(str, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsmiCarga_Click(object sender, EventArgs e)
        {
            string equipa = tvwEquipas.SelectedNode.Tag.ToString();
            List<Tarefa> activas = lstTarefas.Where(x => x.Equipa == equipa && x.Activa())
                                             .OrderBy(x => x.DataFim).ToList();

            string str = "Equipa " + equipa + ": " + activas.Count + " tarefa(s) em curso.\n\n";

            List<string> funcs = lstTarefas.Where(x => x.Equipa == equipa)
                                           .Select(x => x.Funcionario).Distinct().ToList();
            foreach (string f in funcs)
                str += "  " + f + ": " + activas.Count(x => x.Funcionario == f) + "\n";

            if (activas.Count > 0)
            {
                str += "\nTarefa - Funcionário - Fim:\n";
                foreach (Tarefa t in activas)
                    str += "  • " + t.Nome + " - " + t.Funcionario + " - " +
                           t.DataFim.ToString("dd/MM/yyyy") + "\n";
            }
            MessageBox.Show(str, "Carga de trabalho", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void tsmiDesempenho_Click(object sender, EventArgs e)
        {
            string equipa = tvwEquipas.SelectedNode.Tag.ToString();
            List<Tarefa> tarefas = lstTarefas.Where(x => x.Equipa == equipa)
                                             .OrderBy(x => x.Funcionario)
                                             .ThenBy(x => x.DataInicio).ToList();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string path = Ficheiros.EscreverDesempenho(equipa, tarefas);
                MessageBox.Show("Desempenho da equipa " + equipa + " escrito em:\n" + path,
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException ex)
            {
                MostraErro("Não tem permissões para escrever o ficheiro de desempenho.", ex);
            }
            catch (IOException ex)
            {
                MostraErro("Não foi possível escrever o ficheiro de desempenho.\n" +
                    "Verifique se não está aberto noutra aplicação.", ex);
            }
            catch (Exception ex)
            {
                MostraErro("Erro inesperado ao escrever o ficheiro de desempenho.", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void MostraErro(string mensagem, Exception ex)
        {
            Ficheiros.RegistaErro(mensagem, ex);

            string texto = mensagem;
            if (ex != null)
                texto += "\n\nDetalhe técnico: " + ex.Message;
            MessageBox.Show(texto, "Tasks To Do - Erro", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}

