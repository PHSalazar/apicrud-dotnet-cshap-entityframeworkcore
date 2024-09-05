using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

namespace ApiCrud.Estudantes
{
    public static class EstudanteRotas
    {
        public static void AddRotasEstudantes (this WebApplication app)
        {
            // Adicionar Estudante
            app.MapPost("estudantes", async (AddEstudanteRequest request, AppDbContext context, CancellationToken ct) =>
            {
                 var estudanteJaExiste = await context.Estudantes.AnyAsync(estudante => estudante.Nome == request.Nome);

                if (estudanteJaExiste)
                    return Results.Conflict(request.Nome + " já existe no banco de dados.");


                var novoEstudate = new Estudante(request.Nome);
                await context.Estudantes.AddAsync(novoEstudate, ct);
                await context.SaveChangesAsync(ct);

                var estudanteRetorno = new EstudanteDto(novoEstudate.Id, novoEstudate.Nome);
                return Results.Ok(estudanteRetorno);
            });

            // Obter lista de Estudantes
            app.MapGet("estudantes", async (AppDbContext context, CancellationToken ct) =>
            {
                var estudantes = await context.Estudantes
                    .Where(estudante => estudante.Ativo)
                    .Select(estudante => new EstudanteDto(estudante.Id, estudante.Nome))
                    .ToListAsync(ct);
                return estudantes;
            });

            // Atualizar Estudante (Nome)
            app.MapPut("estudante/{id:guid}", async (Guid id,UpdateEstudanteRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id);

                // Não encontrado
                if (estudante == null)
                    return Results.NotFound();

                // Encontrado e atualizar
                estudante.AtualizarNome(request.Nome);

                // Salvar modificação
                await context.SaveChangesAsync(ct);

                return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome));
            });


            // Deletar/Desativar um estudante
            app.MapDelete("estudante/{id}", async (Guid id, AppDbContext context, CancellationToken ct) =>
            {
                var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id);

                // Não encontrou o estudante para desativar o status
                if (estudante == null)
                    return Results.NotFound();

                estudante.DesativarEstudante();
                await context.SaveChangesAsync(ct); // Salvar
                return Results.Ok();
            });
        }
    }
}
