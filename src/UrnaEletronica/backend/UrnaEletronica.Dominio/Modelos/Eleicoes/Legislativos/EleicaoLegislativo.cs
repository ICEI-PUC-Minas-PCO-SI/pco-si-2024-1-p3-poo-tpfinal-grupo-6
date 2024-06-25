
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Dominio.Modelos.Resultados;

namespace UrnaEletronica.Dominio.Modelos.Eleicoes.Legislativos
{
    public class EleicaoLegislativo : Eleicao
    {
        public string TipoLegislativo { get; set; }
        public override List<Resultado> CalcularResultado(ParametroEleicao parametroEleicao, IEnumerable<Candidato> candidatos)
        {

            List<Resultado> resultados = new List<Resultado>();

            int totalVotos = candidatos.Sum(c => c.QtdVotos);
            int quocienteEleitoral = totalVotos / parametroEleicao.QtdCadeiras;

            var coligacoes = candidatos.GroupBy(c => c.ColigacaoId);

            var quocientesPartidarios = coligacoes.Select(g => new
            {
                coligacaoId = g.Key,
                QuocientePartidario = g.Sum(c => c.QtdVotos) / quocienteEleitoral,
                Candidatos = g.ToList()
            }).ToList();

            foreach (var q in quocientesPartidarios)
            {
                int cadeirasObtidas = q.QuocientePartidario;

                var candidatosOrdenados = q.Candidatos
                    .OrderByDescending(c => c.QtdVotos)
                    .Take(cadeirasObtidas)
                    .ToList();

                foreach (var candidato in candidatosOrdenados)
                {
                    resultados = new List<Resultado>
                    {
                        new Resultado
                        {
                            CandidatoId = candidato.Id,
                            QtdVotos = candidato.QtdVotos,
                            PercentualVotos = (double)candidato.QtdVotos / totalVotos * 100,
                            CandidatoEleito = true
                        }
                    };
                };


                ///Distribuir sobras
                int cadeirasDistribuidas = quocientesPartidarios.Sum(q => q.QuocientePartidario);
                int cadeirasRestantes = parametroEleicao.QtdCadeiras - cadeirasDistribuidas;

                if (cadeirasRestantes > 0)
                {
                    var coligacoesRestantesSobra = quocientesPartidarios.OrderByDescending(q => q.Candidatos.Sum(c => c.QtdVotos) / (q.QuocientePartidario + 1)).ToList();

                    for (int i = 0; i < cadeirasRestantes; i++)
                    {
                        var qc = coligacoesRestantesSobra[i].QuocientePartidario + 1;

                        var candidato = coligacoesRestantesSobra[i].Candidatos
                            .OrderByDescending(c => c.QtdVotos)
                            .Skip(coligacoesRestantesSobra[i].QuocientePartidario - 1)
                            .FirstOrDefault();

                        if (candidato != null)
                        {
                            resultados = new List<Resultado>
                            {
                                new Resultado
                                {
                                    CandidatoId = candidato.Id,
                                    QtdVotos = candidato.QtdVotos,
                                    PercentualVotos = (double)candidato.QtdVotos / totalVotos * 100,
                                    CandidatoEleito = true
                                }
                            };
                        }
                    }
                }
            }
            return resultados;
        }
    }
}
