using System.ComponentModel.DataAnnotations;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesUnitarios.Utilitarios;

/// <summary>
/// Classe para aplicar validação do model state.
/// </summary>
public static class ModelStateValidator
{
  /// <summary>
  /// Valida uma determinada model usando model state validation.
  /// </summary>
  /// <typeparam name="T">Tipo de model para validação.</typeparam>
  /// <param name="model">Model.</param>
  /// <returns>Resultado de validação.</returns>
  public static IList<ValidationResult> Validate<T>(T model)
  {
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(model!, null, null);
    Validator.TryValidateObject(model!, validationContext, validationResults, true);

    return validationResults;
  }
}
