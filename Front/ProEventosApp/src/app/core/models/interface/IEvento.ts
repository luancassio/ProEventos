import { Lote } from "./ILote";
import { Palestrante } from "./IPalestrante";
import { RedeSocial } from "./IRedeSocial";

export interface Evento {
    id: number;
    local: string;
    dataEvento?: Date;
    tema: string;
    qtdPessoa: number;
    imageUrl: string;
    telefone: string;
    email: string;
    lotes: Lote[];
    redeSociais: RedeSocial[];
    palestrantesEventos: Palestrante[];
}
