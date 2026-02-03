using System.Runtime.CompilerServices;

public class CoreEngine(List<IFeatureProcessor> featureProcessors)
{
    public double CalculaScore(EnderecoModel inputAddress, EnderecoModel baseAddress)
    {
        var score = 0d;
        var pesoTotal = 0;

        foreach(var feature in featureProcessors)
        {
            pesoTotal += feature.Peso;

            score += feature.CalculaScore(inputAddress, baseAddress) * feature.Peso;
        }

        return score / pesoTotal;
    } 
} 