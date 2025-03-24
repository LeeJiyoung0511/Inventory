public class Character
{
    public Character(float baseAp, float baseDP, float baseHP)
    {
        AttackPower = baseAp;
        DefensePower = baseDP;
        HP = baseHP;
    }

    public float AttackPower { get; private set; }
    public float DefensePower { get; private set; }
    public float HP { get; private set; }

    //공격력 조정
    public void ModifyAttackPower(float add) => AttackPower += add;
    //방어력 조정
    public void ModifyDefensePower(float add) => DefensePower += add;
    //체력 조정
    public void ModifyHP(float add) => HP += add;
}
