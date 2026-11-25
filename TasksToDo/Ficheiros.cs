using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksToDo
{
    public static class Ficheiros
    {
        public static readonly string Pasta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TasksToDo");

        public static readonly string FichFuncionarios = Path.Combine(Pasta, "funcionarios.txt");
        public static readonly string FichEquipas = Path.Combine(Pasta, "equipas.txt");
        public static readonly string FichTarefas = Path.Combine(Pasta, "tarefas.txt");
        public static readonly string FichLog = Path.Combine(Pasta, "erros.log");

        public static List<string> LerNomes(string path)
        {
            List<string> nomes = new List<string>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path);
                while (sr.EndOfStream == false)
                {
                    string linha = sr.ReadLine().Trim();
                    if (linha != "" && nomes.Contains(linha) == false)
                        nomes.Add(linha);
                }
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return nomes;
        }

        public static void EscreverNomes(string path, List<string> nomes)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (string nome in nomes)
                    sw.WriteLine(nome);
            }
        }

        public static List<Tarefa> LerTarefas(out int invalidas)
        {
            List<Tarefa> lista = new List<Tarefa>();
            invalidas = 0;

            if (File.Exists(FichTarefas) == false)
                return lista;

            StreamReader sr = null;
            int numLinha = 0;
            try
            {
                sr = new StreamReader(FichTarefas);
                while (sr.EndOfStream == false)
                {
                    string linha = sr.ReadLine();
                    numLinha++;
                    if (linha.Trim() == "")
                        continue;
                    try
                    {
                        lista.Add(Tarefa.FromLinha(linha));
                    }
                    catch (FormatException ex)
                    {
                        invalidas++;
                        RegistaErro("Linha " + numLinha + " do ficheiro " + FichTarefas +
                            " ignorada.", ex);
                    }
                }
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return lista;
        }

        public static void EscreverTarefas(List<Tarefa> lista)
        {
            using (StreamWriter sw = new StreamWriter(FichTarefas))
            {
                foreach (Tarefa t in lista)
                    sw.WriteLine(t.ToLinha());
            }
        }

        public static string EscreverDesempenho(string equipa, List<Tarefa> tarefas)
        {
            string nomeFich = equipa;
            foreach (char c in Path.GetInvalidFileNameChars())
                nomeFich = nomeFich.Replace(c, '_');
            string path = Path.Combine(Pasta, "desempenho_" + nomeFich + ".txt");

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("DESEMPENHO DA EQUIPA " + equipa.ToUpper());
                sw.WriteLine("Gerado em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                sw.WriteLine("Total de tarefas: " + tarefas.Count);
                sw.WriteLine(new string('=', 60));

                string funcAnterior = "";
                foreach (Tarefa t in tarefas)
                {
                    if (t.Funcionario != funcAnterior)
                    {
                        sw.WriteLine();
                        sw.WriteLine("Funcionário: " + t.Funcionario +
                            (t.Coordenador ? " (coordenador da equipa)" : ""));
                        sw.WriteLine(new string('-', 60));
                        funcAnterior = t.Funcionario;
                    }
                    sw.WriteLine("  Tarefa:      " + t.Nome);
                    sw.WriteLine("  Responsável: " + (t.Responsavel ? "Sim" : "Não"));
                    sw.WriteLine("  Início:      " + t.DataInicio.ToString("dd/MM/yyyy"));
                    sw.WriteLine("  Fim:         " + t.DataFim.ToString("dd/MM/yyyy"));
                    sw.WriteLine("  Duração:     " + ((t.DataFim - t.DataInicio).Days + 1) + " dia(s)");
                    sw.WriteLine("  Estado:      " + t.Estado());
                    sw.WriteLine("  Descrição:   " +
                        (t.Descricao == "" ? "(sem descrição)" : t.Descricao.Replace(Environment.NewLine, " ")));
                    sw.WriteLine();
                }
            }
            return path;
        }

        public static void RegistaErro(string mensagem, Exception ex)
        {
            string linha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | " +
                           mensagem.Replace("\n", " ");
            if (ex != null)
            {
                linha += " | " + ex.GetType().Name + ": " + ex.Message;
                if (ex.TargetSite != null)
                    linha += " | Método: " + ex.TargetSite.Name;
            }
            EscreveLog(linha);
        }

        public static void RegistaInicio()
        {
            EscreveLog("===== " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                " | Início da aplicação (utilizador " + Environment.UserName + ") =====");
        }

        private static void EscreveLog(string linha)
        {
            try
            {
                if (Directory.Exists(Pasta) == false)
                    Directory.CreateDirectory(Pasta);
                using (StreamWriter sw = new StreamWriter(FichLog, true))
                    sw.WriteLine(linha);
            }
            catch (Exception)
            {
            }
        }
    }
}
