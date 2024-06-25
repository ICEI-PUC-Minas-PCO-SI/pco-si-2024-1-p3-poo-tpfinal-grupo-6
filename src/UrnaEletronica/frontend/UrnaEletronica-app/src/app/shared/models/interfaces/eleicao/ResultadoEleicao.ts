import { Candidato } from "../candidato";
import { Partido } from "../partido";

export interface ResultadoEleicao {
    id: number;
    candidatoId: number;
    candidato: Candidato;
    qtdVotos: number;
    percentualVotos: number;
    candidatoEleito: boolean;
}
