import { Candidato } from "../candidato";
import { Coligacao } from "../coligacao";

export interface Partido {
    id: number;
    nome: string;
    sigla: string;
    coligacaoId: number;
    coligacao: Coligacao;
    candidatos: Candidato[];
}
