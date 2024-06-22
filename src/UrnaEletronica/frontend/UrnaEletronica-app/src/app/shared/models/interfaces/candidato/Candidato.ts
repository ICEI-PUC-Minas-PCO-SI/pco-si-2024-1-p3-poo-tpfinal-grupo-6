export interface Candidato {
  id: number;
  ehExecutivo: boolean;
  ehLegislativo: boolean;
  nome: string;
  qtdVotos: number;
  votosValidos: boolean;
  dataNascimento: string;
  tipoCandidatura: string;
  fotoURL: string;
  cidadeId: number;
  //cidade: Cidade;
  partidoId: number;
  //partido: Partido;
  coligacaoId: number;
  //coligacao: Coligacao;
}
