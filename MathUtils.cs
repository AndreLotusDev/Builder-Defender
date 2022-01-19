using System;

public static class MathUtils 
{
    public static float CalculsAPercentagemOfANumber(float percentage, float total)
    {
        if(percentage > 0)
            return (float)(total * percentage) / 100 ;

        return 0;
    }
}
