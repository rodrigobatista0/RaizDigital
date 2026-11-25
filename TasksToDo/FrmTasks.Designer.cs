namespace TasksToDo
{
    partial class FrmTasks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblFuncionario = new System.Windows.Forms.Label();
            this.cbbFuncionario = new System.Windows.Forms.ComboBox();
            this.lblEquipa = new System.Windows.Forms.Label();
            this.cbbEquipa = new System.Windows.Forms.ComboBox();
            this.lblTarefa = new System.Windows.Forms.Label();
            this.txtTarefa = new System.Windows.Forms.TextBox();
            this.chkCoordenador = new System.Windows.Forms.CheckBox();
            this.chkResponsavel = new System.Windows.Forms.CheckBox();
            this.lblInicio = new System.Windows.Forms.Label();
            this.mtxtInicio = new System.Windows.Forms.MaskedTextBox();
            this.lblFim = new System.Windows.Forms.Label();
            this.mtxtFim = new System.Windows.Forms.MaskedTextBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.tvwEquipas = new System.Windows.Forms.TreeView();
            this.cmsTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiApagar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDetalhes = new System.Windows.Forms.ToolStripMenuItem();
            this.tssSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiActivas = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLista = new System.Windows.Forms.ToolStripMenuItem();
            this.tssSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCarga = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDesempenho = new System.Windows.Forms.ToolStripMenuItem();
            this.erpUI = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmsTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpUI)).BeginInit();
            this.SuspendLayout();
            //
            // lblFuncionario
            //
            this.lblFuncionario.AutoSize = true;
            this.lblFuncionario.Location = new System.Drawing.Point(12, 12);
            this.lblFuncionario.Name = "lblFuncionario";
            this.lblFuncionario.Size = new System.Drawing.Size(62, 13);
            this.lblFuncionario.TabIndex = 0;
            this.lblFuncionario.Text = "Funcionário";
            //
            // cbbFuncionario
            //
            this.cbbFuncionario.FormattingEnabled = true;
            this.cbbFuncionario.Location = new System.Drawing.Point(12, 28);
            this.cbbFuncionario.Name = "cbbFuncionario";
            this.cbbFuncionario.Size = new System.Drawing.Size(300, 21);
            this.cbbFuncionario.Sorted = true;
            this.cbbFuncionario.TabIndex = 1;
            this.cbbFuncionario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Texto_KeyPress);
            //
            // lblEquipa
            //
            this.lblEquipa.AutoSize = true;
            this.lblEquipa.Location = new System.Drawing.Point(12, 58);
            this.lblEquipa.Name = "lblEquipa";
            this.lblEquipa.Size = new System.Drawing.Size(40, 13);
            this.lblEquipa.TabIndex = 2;
            this.lblEquipa.Text = "Equipa";
            //
            // cbbEquipa
            //
            this.cbbEquipa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEquipa.FormattingEnabled = true;
            this.cbbEquipa.Location = new System.Drawing.Point(12, 74);
            this.cbbEquipa.Name = "cbbEquipa";
            this.cbbEquipa.Size = new System.Drawing.Size(300, 21);
            this.cbbEquipa.Sorted = true;
            this.cbbEquipa.TabIndex = 3;
            //
            // lblTarefa
            //
            this.lblTarefa.AutoSize = true;
            this.lblTarefa.Location = new System.Drawing.Point(12, 104);
            this.lblTarefa.Name = "lblTarefa";
            this.lblTarefa.Size = new System.Drawing.Size(39, 13);
            this.lblTarefa.TabIndex = 4;
            this.lblTarefa.Text = "Tarefa";
            //
            // txtTarefa
            //
            this.txtTarefa.Location = new System.Drawing.Point(12, 120);
            this.txtTarefa.MaxLength = 30;
            this.txtTarefa.Name = "txtTarefa";
            this.txtTarefa.Size = new System.Drawing.Size(300, 20);
            this.txtTarefa.TabIndex = 5;
            this.txtTarefa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Texto_KeyPress);
            //
            // chkCoordenador
            //
            this.chkCoordenador.AutoSize = true;
            this.chkCoordenador.Location = new System.Drawing.Point(12, 150);
            this.chkCoordenador.Name = "chkCoordenador";
            this.chkCoordenador.Size = new System.Drawing.Size(86, 17);
            this.chkCoordenador.TabIndex = 6;
            this.chkCoordenador.Text = "Coordenador";
            this.chkCoordenador.UseVisualStyleBackColor = true;
            //
            // chkResponsavel
            //
            this.chkResponsavel.AutoSize = true;
            this.chkResponsavel.Location = new System.Drawing.Point(172, 150);
            this.chkResponsavel.Name = "chkResponsavel";
            this.chkResponsavel.Size = new System.Drawing.Size(88, 17);
            this.chkResponsavel.TabIndex = 7;
            this.chkResponsavel.Text = "Responsável";
            this.chkResponsavel.UseVisualStyleBackColor = true;
            //
            // lblInicio
            //
            this.lblInicio.AutoSize = true;
            this.lblInicio.Location = new System.Drawing.Point(12, 178);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(76, 13);
            this.lblInicio.TabIndex = 8;
            this.lblInicio.Text = "Data de início";
            //
            // mtxtInicio
            //
            this.mtxtInicio.Location = new System.Drawing.Point(12, 194);
            this.mtxtInicio.Mask = "00/00/0000";
            this.mtxtInicio.Name = "mtxtInicio";
            this.mtxtInicio.Size = new System.Drawing.Size(130, 20);
            this.mtxtInicio.TabIndex = 9;
            this.mtxtInicio.ValidatingType = typeof(System.DateTime);
            //
            // lblFim
            //
            this.lblFim.AutoSize = true;
            this.lblFim.Location = new System.Drawing.Point(172, 178);
            this.lblFim.Name = "lblFim";
            this.lblFim.Size = new System.Drawing.Size(66, 13);
            this.lblFim.TabIndex = 10;
            this.lblFim.Text = "Data de fim";
            //
            // mtxtFim
            //
            this.mtxtFim.Location = new System.Drawing.Point(172, 194);
            this.mtxtFim.Mask = "00/00/0000";
            this.mtxtFim.Name = "mtxtFim";
            this.mtxtFim.Size = new System.Drawing.Size(130, 20);
            this.mtxtFim.TabIndex = 11;
            this.mtxtFim.ValidatingType = typeof(System.DateTime);
            //
            // lblDescricao
            //
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(12, 224);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(55, 13);
            this.lblDescricao.TabIndex = 12;
            this.lblDescricao.Text = "Descrição";
            //
            // txtDescricao
            //
            this.txtDescricao.AcceptsReturn = true;
            this.txtDescricao.Location = new System.Drawing.Point(12, 240);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricao.Size = new System.Drawing.Size(300, 120);
            this.txtDescricao.TabIndex = 13;
            this.txtDescricao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Texto_KeyPress);
            //
            // btnAdicionar
            //
            this.btnAdicionar.Location = new System.Drawing.Point(12, 374);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(130, 30);
            this.btnAdicionar.TabIndex = 14;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(172, 374);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(130, 30);
            this.btnGuardar.TabIndex = 15;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // tvwEquipas
            //
            this.tvwEquipas.ContextMenuStrip = this.cmsTree;
            this.tvwEquipas.HideSelection = false;
            this.tvwEquipas.Location = new System.Drawing.Point(340, 12);
            this.tvwEquipas.Name = "tvwEquipas";
            this.tvwEquipas.Size = new System.Drawing.Size(400, 392);
            this.tvwEquipas.TabIndex = 16;
            //
            // cmsTree
            //
            this.cmsTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditar,
            this.tsmiApagar,
            this.tsmiDetalhes,
            this.tssSep1,
            this.tsmiActivas,
            this.tsmiLista,
            this.tssSep2,
            this.tsmiCarga,
            this.tsmiDesempenho});
            this.cmsTree.Name = "cmsTree";
            this.cmsTree.Size = new System.Drawing.Size(170, 170);
            this.cmsTree.Opening += new System.ComponentModel.CancelEventHandler(this.cmsTree_Opening);
            //
            // tsmiEditar
            //
            this.tsmiEditar.Name = "tsmiEditar";
            this.tsmiEditar.Size = new System.Drawing.Size(169, 22);
            this.tsmiEditar.Text = "Editar";
            this.tsmiEditar.Click += new System.EventHandler(this.tsmiEditar_Click);
            //
            // tsmiApagar
            //
            this.tsmiApagar.Name = "tsmiApagar";
            this.tsmiApagar.Size = new System.Drawing.Size(169, 22);
            this.tsmiApagar.Text = "Apagar";
            this.tsmiApagar.Click += new System.EventHandler(this.tsmiApagar_Click);
            //
            // tsmiDetalhes
            //
            this.tsmiDetalhes.Name = "tsmiDetalhes";
            this.tsmiDetalhes.Size = new System.Drawing.Size(169, 22);
            this.tsmiDetalhes.Text = "Detalhes";
            this.tsmiDetalhes.Click += new System.EventHandler(this.tsmiDetalhes_Click);
            //
            // tssSep1
            //
            this.tssSep1.Name = "tssSep1";
            this.tssSep1.Size = new System.Drawing.Size(166, 6);
            //
            // tsmiActivas
            //
            this.tsmiActivas.Name = "tsmiActivas";
            this.tsmiActivas.Size = new System.Drawing.Size(169, 22);
            this.tsmiActivas.Text = "Tarefas activas";
            this.tsmiActivas.Click += new System.EventHandler(this.tsmiActivas_Click);
            //
            // tsmiLista
            //
            this.tsmiLista.Name = "tsmiLista";
            this.tsmiLista.Size = new System.Drawing.Size(169, 22);
            this.tsmiLista.Text = "Lista de tarefas";
            this.tsmiLista.Click += new System.EventHandler(this.tsmiLista_Click);
            //
            // tssSep2
            //
            this.tssSep2.Name = "tssSep2";
            this.tssSep2.Size = new System.Drawing.Size(166, 6);
            //
            // tsmiCarga
            //
            this.tsmiCarga.Name = "tsmiCarga";
            this.tsmiCarga.Size = new System.Drawing.Size(169, 22);
            this.tsmiCarga.Text = "Carga de trabalho";
            this.tsmiCarga.Click += new System.EventHandler(this.tsmiCarga_Click);
            //
            // tsmiDesempenho
            //
            this.tsmiDesempenho.Name = "tsmiDesempenho";
            this.tsmiDesempenho.Size = new System.Drawing.Size(169, 22);
            this.tsmiDesempenho.Text = "Desempenho";
            this.tsmiDesempenho.Click += new System.EventHandler(this.tsmiDesempenho_Click);
            //
            // erpUI
            //
            this.erpUI.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.erpUI.ContainerControl = this;
            //
            // FrmTasks
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 418);
            this.Controls.Add(this.tvwEquipas);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.mtxtFim);
            this.Controls.Add(this.lblFim);
            this.Controls.Add(this.mtxtInicio);
            this.Controls.Add(this.lblInicio);
            this.Controls.Add(this.chkResponsavel);
            this.Controls.Add(this.chkCoordenador);
            this.Controls.Add(this.txtTarefa);
            this.Controls.Add(this.lblTarefa);
            this.Controls.Add(this.cbbEquipa);
            this.Controls.Add(this.lblEquipa);
            this.Controls.Add(this.cbbFuncionario);
            this.Controls.Add(this.lblFuncionario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmTasks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tasks To Do";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTasks_FormClosing);
            this.Load += new System.EventHandler(this.FrmTasks_Load);
            this.cmsTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.erpUI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFuncionario;
        private System.Windows.Forms.ComboBox cbbFuncionario;
        private System.Windows.Forms.Label lblEquipa;
        private System.Windows.Forms.ComboBox cbbEquipa;
        private System.Windows.Forms.Label lblTarefa;
        private System.Windows.Forms.TextBox txtTarefa;
        private System.Windows.Forms.CheckBox chkCoordenador;
        private System.Windows.Forms.CheckBox chkResponsavel;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.MaskedTextBox mtxtInicio;
        private System.Windows.Forms.Label lblFim;
        private System.Windows.Forms.MaskedTextBox mtxtFim;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TreeView tvwEquipas;
        private System.Windows.Forms.ContextMenuStrip cmsTree;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditar;
        private System.Windows.Forms.ToolStripMenuItem tsmiApagar;
        private System.Windows.Forms.ToolStripMenuItem tsmiDetalhes;
        private System.Windows.Forms.ToolStripSeparator tssSep1;
        private System.Windows.Forms.ToolStripMenuItem tsmiActivas;
        private System.Windows.Forms.ToolStripMenuItem tsmiLista;
        private System.Windows.Forms.ToolStripSeparator tssSep2;
        private System.Windows.Forms.ToolStripMenuItem tsmiCarga;
        private System.Windows.Forms.ToolStripMenuItem tsmiDesempenho;
        private System.Windows.Forms.ErrorProvider erpUI;
    }
}
