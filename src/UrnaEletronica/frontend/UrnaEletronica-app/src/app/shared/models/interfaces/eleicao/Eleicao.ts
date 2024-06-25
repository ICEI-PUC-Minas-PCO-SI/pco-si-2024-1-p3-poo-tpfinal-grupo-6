import { Candidato } from "../candidato";
import { Partido } from "../partido";

export interface Eleicao {
    id: number;
    tipoEleicao: string;
    iniciarVotacao: boolean;
    encerrarVotacao: boolean;
    dataHoraInicioVotacao: Date;
    dataHoraFimVotacao: Date;
}
