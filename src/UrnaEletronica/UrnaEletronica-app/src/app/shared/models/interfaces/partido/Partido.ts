// import { Patrimonio } from "../patrimonio";

/*mudar pra partido dto*/ 
export interface Acervo {
  id: number;
  nome: string;
  sigla: string;
  coligacaoId: number;
  //coligacao: Coligacao[];
  //candidatos: Candidato[];
}
