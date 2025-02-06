using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Extensoes;

public static class ModelStateExtensions
{
  public static IEnumerable<string> GetMensagensErro(this ModelStateDictionary modelState)
    => modelState.SelectMany(m => m.Value!.Errors).Select(m => m.ErrorMessage);
}
