using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksToDo
{
    public class Tarefa
    {
        public string Equipa { get; set; }
        public string Funcionario { get; set; }
        public bool Coordenador { get; set; }
        public string Nome { get; set; }
        public bool Responsavel { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Descricao { get; set; }

        public bool Activa()
        {
            return DataInicio <= DateTime.Today && DataFim >= DateTime.Today;
        }

        public string Estado()
        {
            if (DataFim < DateTime.Today)
                return "concluída";
            if (DataInicio > DateTime.Today)
                return "por iniciar";
            return "em curso";
        }

        public string ToLinha()
        {
            string desc = Descricao.Replace("\r\n", "\\n").Replace("\n", "\\n");
            return Equipa + ";" + Funcionario + ";" + Coordenador + ";" + Nome + ";" +
                   Responsavel + ";" +
                   DataInicio.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + ";" +
                   DataFim.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + ";" +
                   desc;
        }

        public static Tarefa FromLinha(string linha)
        {
            string[] campos = linha.Split(';');
            if (campos.Length != 8)
                throw new FormatException("A linha tem " + campos.Length +
                    " campos em vez de 8.");

            Tarefa t = new Tarefa();
            t.Equipa = campos[0].Trim();
            t.Funcionario = campos[1].Trim();
            t.Coordenador = Convert.ToBoolean(campos[2].Trim());
            t.Nome = campos[3].Trim();
            t.Responsavel = Convert.ToBoolean(campos[4].Trim());
            t.DataInicio = DateTime.ParseExact(campos[5].Trim(), "dd/MM/yyyy",
                CultureInfo.InvariantCulture);
            t.DataFim = DateTime.ParseExact(campos[6].Trim(), "dd/MM/yyyy",
                CultureInfo.InvariantCulture);
            t.Descricao = campos[7].Replace("\\n", Environment.NewLine);

            if (t.Equipa == "" || t.Funcionario == "" || t.Nome == "")
                throw new FormatException("Equipa, funcionário ou tarefa vazios.");
            if (t.Nome.Length > 30)
                throw new FormatException("O nome da tarefa tem mais de 30 caracteres.");
            if (t.DataFim < t.DataInicio)
                throw new FormatException("A data de fim é anterior à data de início.");

            return t;
        }
    }
}
