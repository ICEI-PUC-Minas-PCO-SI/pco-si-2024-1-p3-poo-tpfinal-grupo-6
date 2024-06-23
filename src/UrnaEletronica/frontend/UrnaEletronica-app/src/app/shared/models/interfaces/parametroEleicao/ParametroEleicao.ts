import { Cidade } from "../cidade";

export interface ParametroEleicao {
    id: number;
    primeiroTurno: boolean;
    segundoTurno: boolean;
    qtdVotosSomentePrimeiroTurno: number;
    qtdCadeiras: number;
    dataEleicaoPrimeiroTurno: Date,
    dataEleicaoSegundoTurno: Date,
    cidadeId: number;
    cidade: Cidade;
}
