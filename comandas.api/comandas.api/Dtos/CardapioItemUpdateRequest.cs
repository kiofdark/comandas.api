﻿namespace comandas.api.Dtos
{
    public class CardapioItemUpdateRequest
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool PossuiPreparo { get; set; }
    }
}
