using System.Text.RegularExpressions;

Dictionary<string, string> synonymsTable = new(StringComparer.OrdinalIgnoreCase)
{
    // Address Types
    { "R.", "Rua" },
    { "R", "Rua" },
    { "Av.", "Avenida" },
    { "Av", "Avenida" },
    { "Rod.", "Rodovia" },
    { "Rod", "Rodovia" },
    { "Pça", "Praça" },
    { "Pça.", "Praça" },
    { "Al.", "Alameda" },
    { "Trav.", "Travessa" },

    // Complements
    { "Apto", "Apartamento" },
    { "Ap", "Apartamento" },
    { "Bl", "Bloco" },
    { "Conj", "Conjunto" },
    { "Sl", "Sala" },

    // States/Cities
    { "SP", "São Paulo" },
    { "RJ", "Rio de Janeiro" },
    { "MG", "Minas Gerais" },
    { "BH", "Belo Horizonte" },
    
    // Join Symbols
    { "-", "" }
};

List<Dictionary<string, string>> addressDatabase =
[
    new() { { "ID", "1" }, { "Logradouro", "Av. Paulista" }, { "Numero", "1000" }, { "Bairro", "Bela Vista" }, { "Cidade", "São Paulo" }, { "UF", "SP" }, { "CEP", "30520-330" } },
    new() { { "ID", "2" }, { "Logradouro", "R. das Flores" }, { "Numero", "123" }, { "Complemento", "Ap 402" }, { "Cidade", "Desconhecida" }, { "UF", "Desconhecida" } },
    new() { { "ID", "3" }, { "Logradouro", "Pça da Liberdade" }, { "Numero", "S/N" }, { "Cidade", "BH" }, { "UF", "MG" } },
    new() { { "ID", "4" }, { "Logradouro", "RODOVIA ANCHIETA" }, { "Numero", "KM 15" }, { "UF", "SP" } },
    new() { { "ID", "5" }, { "Logradouro", "Alameda Santos" }, { "Numero", "450" }, { "Complemento", "10 ANDAR, SL 101" }, { "Cidade", "São Paulo" }, { "UF", "SP" } },
    new() { { "ID", "6" }, { "Logradouro", "Trav. do Ouvidor" }, { "Cidade", "Rio de Janeiro" }, { "UF", "RJ" } },
    new() { { "ID", "7" }, { "Logradouro", "Rua Oscar Freire" }, { "Numero", "500" }, { "Cidade", "São Paulo" }, { "UF", "SP" }, { "CEP", "01426001" } },
    new() { { "ID", "8" }, { "Logradouro", "Av. Brig. Faria Lima" }, { "Numero", "3477" }, { "Bairro", "Itaim Bibi" }, { "Cidade", "São Paulo" }, { "UF", "SP" }, { "CEP", "04538-133" } },
    new() { { "ID", "9" }, { "Logradouro", "Estr. dos Bandeirantes" }, { "Numero", "2000" }, { "Bairro", "Jacarepaguá" }, { "Cidade", "Rio de Janeiro" }, { "UF", "RJ" } },
    new() { { "ID", "10" }, { "Logradouro", "Loteamento Portal do Sol" }, { "Quadra", "Qd 12" }, { "Lote", "Lt 05" }, { "Cidade", "Goiânia" }, { "UF", "GO" } },
    new() { { "ID", "11" }, { "Logradouro", "Viela da Paz" }, { "Numero", "10" }, { "Complemento", "Bloco C, Apto 22" } },
    new() { { "ID", "12" }, { "Logradouro", "av beira mar" }, { "Numero", "100" }, { "Bairro", "Meireles" }, { "Cidade", "Fortaleza" }, { "UF", "CE" } },
    new() { { "ID", "13" }, { "Logradouro", "PRACA DA SE" }, { "Numero", "S/N" }, { "Bairro", "CENTRO" }, { "Cidade", "SAO PAULO" }, { "UF", "SP" } },
    new() { { "ID", "14" }, { "Logradouro", "rod. marechal rondon" }, { "Numero", "km 250" }, { "Cidade", "bauru" }, { "UF", "sp" } },
    new() { { "ID", "15" }, { "Logradouro", "Av. Ponta Negra" }, { "Numero", "15" }, { "Cidade", "Manaus" }, { "UF", "AM" }, { "CEP", "69037-000" } },
    new() { { "ID", "16" }, { "Logradouro", "R. XV de Novembro" }, { "Numero", "50" }, { "Cidade", "Curitiba" }, { "UF", "PR" }, { "CEP", "80020-310" } },
    new() { { "ID", "17" }, { "Logradouro", "Av. Oceanica" }, { "Numero", "456" }, { "Cidade", "Salvador" }, { "UF", "BA" }, { "CEP", "40140-130" } },
    new() { { "ID", "18" }, { "Logradouro", "R. Pe. Eustáquio" }, { "Numero", "2500" }, { "Cidade", "BH" }, { "UF", "MG" } },
    new() { { "ID", "19" }, { "Logradouro", "Av. Dr. Arnaldo" }, { "Numero", "150" }, { "Complemento", "Sl. 4" }, { "Cidade", "Sao Paulo" }, { "UF", "SP" } },
    new() { { "ID", "20" }, { "Logradouro", "Passagem Sto Antonio" }, { "Numero", "12" }, { "Cidade", "Belem" }, { "UF", "PA" } },
    new() { { "ID", "21" }, { "Logradouro", "Setor Comercial Sul" }, { "Quadra", "Q. 4" }, { "Bloco", "Bl. A" }, { "Cidade", "Brasília" }, { "UF", "DF" } }
];

string addressInput;
string idInput;
double weight = 0;

Regex synonymRegex = new(@"\b(" + string.Join("|", synonymsTable.Keys.Select(Regex.Escape)) + @")\b", RegexOptions.IgnoreCase);
Regex zipCodeRegex = new(@"\b\d{5}-?\d{3}\b", RegexOptions.Compiled);

string Standardize(string input)
{
    if (string.IsNullOrWhiteSpace(input)) return input;
    return synonymRegex.Replace(input, match =>
        synonymsTable.TryGetValue(match.Value, out string replacement) ? replacement : match.Value);
}

do
{
    Console.WriteLine("Enter the full address:");
    addressInput = Console.ReadLine();

    Console.WriteLine("Enter the record ID:");
    idInput = Console.ReadLine();

} while (string.IsNullOrWhiteSpace(addressInput) || string.IsNullOrWhiteSpace(idInput));

string standardizedInput = Standardize(addressInput);

var zipCodeMatch = zipCodeRegex.Match(standardizedInput);

if (zipCodeMatch.Success)
{
    string foundZipCode = zipCodeMatch.Value;
    Console.WriteLine($"Zip Code detected in input: {foundZipCode}");

    string databaseZipCode = addressDatabase
        .Where(row => row["ID"] == idInput && row.ContainsKey("CEP"))
        .Select(row => row["CEP"])
        .FirstOrDefault();

    if (Standardize(databaseZipCode) == zipCodeMatch.Value)
    {
        weight = 0.5;
        Console.WriteLine($"Success! Weight updated to: {weight}");
    }
    else
    {
        Console.WriteLine("Zip Code or ID do not match the database.");
    }
}
else
{
    Console.WriteLine("No valid Zip Code found in the entered address.");
}