using comandas.api.Data;
using comandas.api.Domain;
using Microsoft.EntityFrameworkCore;

namespace comandas.api
{
    public static class InicializarDados
    {
        public static void Inicializar(ComandasDbContext context)
        {
         
            context.Database.Migrate();
            // Verifica se há dados na tabela Usuarios
            if (!context.Usuarios.Any())
            {
                context.Usuarios.Add(new Usuario
                {
                    Nome = "Admin",
                    Email = "admin@admin.com",
                    Senha = "admin123" // Em um cenário real, a senha deve ser armazen

                });

               
            }

            if (!context.Mesas.Any()) {
                context.Mesas.AddRange(
                    new Mesa { NumeroMesa = 1, SituacaoMesa = 0 },
                    new Mesa { NumeroMesa = 2, SituacaoMesa = 0 },
                    new Mesa { NumeroMesa = 3, SituacaoMesa = 0 }
                );

            }

            if(!context.CardapioItems.Any()) {
                context.CardapioItems.AddRange(
                    new CardapioItem { Titulo = "Coxinha", Descricao = "Coxinha de frango com catupiry", Preco = 8.00m, PossuiPreparo = true },
                    new CardapioItem { Titulo = "X-Tudo", Descricao = "Alface, Tomate, Presunto, Queijo, Ovo, Hamburguer", Preco = 18.00m, PossuiPreparo = true },
                    new CardapioItem { Titulo = "Refrigerante", Descricao = "Refrigerante lata 350ml", Preco = 6.00m, PossuiPreparo = false }
                );
            }
            
            if(!context.Comandas.Any()) {
                context.Comandas.Add(
                    new Comanda { NumeroMesa = 1, NomeCliente = "Caio", SituacaoComanda = 0 , 
                        
                        Itens = new List<ComandaItem>() 
                    {
                        new ComandaItem { CardapioItemId = 1 }, // 1 Coxinhas
                        new ComandaItem { CardapioItemId = 3 }  // 1 Refrigerante
                    } } 
                );
            }


            context.SaveChanges();
            // criar cadastro mesa, criar cadastro comanda
        }
    }
}
