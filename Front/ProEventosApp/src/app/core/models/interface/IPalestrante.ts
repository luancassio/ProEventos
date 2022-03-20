import { Evento } from "./IEvento";
import { RedeSocial } from "./IRedeSocial";

export interface Palestrante {
    id: number;
    nome: string;
    miniCurriculo: string;
    imagemUrl: string;
    telefone: string;
    email: string;
    redeSociais: RedeSocial[];
    palestrantesEventos: Evento[];
}
