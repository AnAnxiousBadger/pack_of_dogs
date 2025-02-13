using Godot;

public class Fate
{
    public enum Luck {VERYUNLUCKY, UNLUCKY, NEUTRAL, LUCKY, VERYLUCKY}
    public string Text { get; set; }
    public Luck LuckScoreRange { get; set; }

    public static Luck GetLuckFromLuckyScore(float luckyScore){
        if(luckyScore <= -5){
            return Luck.VERYUNLUCKY;
        }
        else if(luckyScore <= -1.5f){
            return Luck.UNLUCKY;
        }
        else if(luckyScore < 1.5f){
            return Luck.NEUTRAL;
        }
        else if(luckyScore < 5f){
            return Luck.LUCKY;
        }
        else{
            return Luck.VERYLUCKY;
        }
    }
}