using VEC_poc_adress_comparison.Models;

namespace VEC_poc_adress_comparison.Core.Features;

public class CepFeatureProcessador : IFeatureProcessador
{
    public int Peso => 30;

    public double CalculaScoreFeature(EnderecoModel enderecoEntrada, EnderecoModel enderecoBase)
    {
        throw new NotImplementedException();
    }
}