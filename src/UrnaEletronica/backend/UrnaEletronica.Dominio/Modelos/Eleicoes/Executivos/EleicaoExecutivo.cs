using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Dominio.Modelos.Resultados;

namespace UrnaEletronica.Dominio.Modelos.Eleicoes.Executivos
{
    public class EleicaoExecutivo : Eleicao
    {
        public string TipoExecutivo { get; set; }

        public override IEnumerable<Resultado> CalcularResultado(ParametroEleicao parametroEleicao, IEnumerable<Candidato> candidatos)
        {
            List<Resultado> resultados = [];

            int totalVotos = candidatos.Sum(c => c.QtdVotos);
            int metadeVotos = totalVotos / 2;
            int candidatoIdEleito = 0;
            bool candidatoEleito = false;


            foreach (Candidato candidato in candidatos)
            {

                if (!candidatoEleito)
                {
                    if (candidato.QtdVotos > metadeVotos)
                    {
                        candidatoIdEleito = candidato.Id;
                    }
                    else if (candidato.Cidade.QtdHabitantes > parametroEleicao.QtdVotosSomentePrimeiroTurno || parametroEleicao.SegundoTurno)
                    {
                        candidatoIdEleito = CalcularMaiorIdade(candidatos);
                    }

                    if (candidato.Id == candidatoIdEleito) candidatoEleito = true;
                    else candidatoEleito = false;
                }


                resultados = new List<Resultado>
                {
                    new Resultado
                    {
                        CandidatoId = candidato.Id,
                        QtdVotos = candidato.QtdVotos,
                        PercentualVotos = candidato.QtdVotos / totalVotos * 100,
                        CandidatoEleito = candidatoEleito
                    }
                };
            }

            return resultados.ToArray();
        }

        public string Executivo()
        {
            return "Prefeito";
        }

        private void setTipoExecutivo()
        {
            TipoExecutivo = Executivo();
        }

        private int CalcularMaiorIdade(IEnumerable<Candidato> candidatos)
        {
            var candidatosOrdenados = candidatos.OrderByDescending(c => c.QtdVotos).ToList();

            var candidatosDesampate = candidatosOrdenados.Take(2).ToList();

            return candidatosDesampate[0].DataNascimento > candidatosDesampate[1].DataNascimento ? candidatosDesampate[0].Id : candidatosDesampate[1].Id;
        }

        public override bool IniciarEleicao()
        {
            try
            {
                if (!base.IniciarVotacao && !base. EncerrarVotacao)
                {
                    base.DataHoraInicioVotacao = DateTime.Now;
                    base.IniciarVotacao = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao inciar votacao. Erro: ${ex.Message}");
            }
        }

        public override bool EncerrarEleicao()
        {
            if (base.IniciarVotacao && !base.EncerrarVotacao)
            {
               base.DataHoraFimVotacao = DateTime.Now;
               base. EncerrarVotacao = true;
                return true;
            }
            else return false;
        }
        public override bool EleicaoEmAdamento()
        {
            return base.IniciarVotacao;
        }

        public override bool EleicaoEncerrada()
        {
            return base.EncerrarVotacao;
        }
    }
}
