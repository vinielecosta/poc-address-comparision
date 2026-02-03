using VEC_poc_adress_comparison.Core.Features;
using VEC_poc_adress_comparison.Models;

namespace VEC_poc_adress_comparison.Core;

public class EngineProcessador(List<IFeatureProcessador> featureProcessadores)
{
    public double CalculaNotaFinal(EnderecoModel enderecoEntrada, EnderecoModel enderecoBase)
    {
        var nota = 0d;
        var pesoTotal = 0;

        foreach (var feature in featureProcessadores)
        {
            pesoTotal += feature.Peso;

            nota += feature.CalculaScoreFeature(enderecoEntrada, enderecoBase) * feature.Peso;
        }

        return nota / pesoTotal;
    }
}