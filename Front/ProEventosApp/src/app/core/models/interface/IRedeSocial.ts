import { Evento } from "./IEvento";
import { Palestrante } from "./IPalestrante";

export interface RedeSocial {
    id: number;
    nome: string;
    url: string;
    eventoId?: number;
    palestranteId?: number;
}
