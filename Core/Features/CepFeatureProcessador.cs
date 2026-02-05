using F23.StringSimilarity;
using VEC_poc_adress_comparison.Models;

namespace VEC_poc_adress_comparison.Core.Features;

public class CepFeatureProcessador() : IFeatureProcessador
{
    public int Peso { get; set; } = 30;
    private static readonly JaroWinkler _Jw = new();

    public double CalculaScoreFeature(EnderecoModel enderecoEntrada, EnderecoModel enderecoBase)
    {
        var pesoFinal = 45;

        var scoreInicio = _Jw.Similarity(enderecoEntrada.CEP.InicioCep, enderecoBase.CEP.InicioCep) * 20;

        var scoreFim = _Jw.Similarity(enderecoEntrada.CEP.FimCep, enderecoBase.CEP.FimCep) * 10;

        return (scoreInicio + scoreFim) / pesoFinal;
    }
}