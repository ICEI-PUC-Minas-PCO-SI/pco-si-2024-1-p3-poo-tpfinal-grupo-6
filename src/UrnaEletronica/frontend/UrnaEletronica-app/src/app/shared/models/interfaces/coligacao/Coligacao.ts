import { Candidato } from "../candidato";
import { Partido } from "../partido";

export interface Coligacao {
    id: number;
    nome: string;
    sigla: string;
    qtdVotos: number;
    partidos: Partido[];
    candidatos: Candidato[];
}
