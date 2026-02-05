using System.Text.RegularExpressions;

namespace VEC_poc_adress_comparison.Core.Features;

public class EngineNormalizador
{
    private readonly Dictionary<string, string> _synonymsTable;
    private readonly Regex _synonymRegex;
    private readonly Regex _cepRegex;
    public EngineNormalizador()
    {
        _synonymsTable = new(StringComparer.OrdinalIgnoreCase)
        {
            // Address Types
            { "R", "Rua" },
            { "Av.", "Avenida" },
            { "Av", "Avenida" },
            { "Rod", "Rodovia" },
            { "Pça", "Praça" },
            { "Pça.", "Praça" },
            { "Al", "Alameda" },
            { "Trav", "Travessa" },
    
            // Complements
            { "Apto", "Apartamento" },
            { "Ap", "Apartamento" },
            { "Bl", "Bloco" },
            { "Conj", "Conjunto" },
            { "Sl", "Sala" },
    
            // Join Symbols
            { "-", "" }
        };

        _synonymRegex = new(@"\b(" + string.Join("|", _synonymsTable.Keys.Select(Regex.Escape)) + @")\b", RegexOptions.IgnoreCase);

        _cepRegex = new(@"^[\s]+|[^a-zA-Z0-9\s]|[\s]+$");
    }
    public string Padronizar(string entrada)
    {
        if (string.IsNullOrWhiteSpace(entrada)) return entrada;

        _cepRegex.Replace(entrada, "");

        return _synonymRegex.Replace(entrada, match =>
            _synonymsTable.TryGetValue(match.Value, out string replacement) ? replacement : match.Value);
    }
}