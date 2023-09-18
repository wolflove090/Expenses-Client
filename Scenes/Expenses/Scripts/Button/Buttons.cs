using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Food : IButton
{
    public string _label => "食費";
    public string _iconName => "btn_line_black_cart";
}

public struct Costco : IButton
{
    public string _label => "コストコ";
    public string _iconName => "btn_line_black_shopping_2";
}

public struct Gasoline : IButton
{
    public string _label => "ガソリン";
    public string _iconName => "btn_line_black_potion";
}

public struct Item : IButton
{
    public string _label => "日用品";
    public string _iconName => "btn_line_black_home";
}

public struct Restaurant : IButton
{
    public string _label => "外食";
    public string _iconName => "btn_line_black_shop";
}

public struct Convenience : IButton
{
    public string _label => "コンビニ";
    public string _iconName => "btn_line_black_restore";
}

public struct Lunch : IButton
{
    public string _label => "お昼";
    public string _iconName => "btn_line_black_bag";
}

public struct Beauty : IButton
{
    public string _label => "美容・服飾";
    public string _iconName => "btn_line_black_jewel_1";
}

public struct Helth : IButton
{
    public string _label => "医療・健康";
    public string _iconName => "btn_line_black_life";
}

public struct Game : IButton
{
    public string _label => "ゲーム";
    public string _iconName => "btn_line_black_game_1";
}

public struct Entertainment : IButton
{
    public string _label => "娯楽・レジャー";
    public string _iconName => "btn_line_black_flag";
}

public struct Study : IButton
{
    public string _label => "教養";
    public string _iconName => "btn_line_black_lamp";
}

public struct Present : IButton
{
    public string _label => "交際費";
    public string _iconName => "btn_line_black_gift";
}

public struct Tatsuki : IButton
{
    public string _label => "樹私用";
    public string _iconName => "btn_line_black_man";
}

public struct Aki : IButton
{
    public string _label => "亜紀私用";
    public string _iconName => "btn_line_black_woman";
}