import { Candidato } from "../candidato";

export interface Partido {
    id: number;
    nome: string;
    sigla: string;
    coligacaoId: number;
    //coligacao: Coligacao[];
    candidatos: Candidato[];
}
