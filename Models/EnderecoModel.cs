namespace VEC_poc_adress_comparison.Models;

public record EnderecoModel(
    CepModel Cep,
    int Numero,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cidade,
    UF UF);