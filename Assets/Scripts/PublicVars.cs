using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVars : MonoBehaviour
{
    public static int bulletDMG = 2;
    public static int score = 0;

    public static int total_xp = 0;
    public static int starting_xp = 0;

    public static int atk_cost = 5;
    public static int atkSpd_cost = 5;
    public static int jmp_cost = 5;
    public static int spd_cost = 5;
    public static int health_cost = 5;

    public static int starting_atk_cost = 5;
    public static int starting_atkSpd_cost = 5;
    public static int starting_jmp_cost = 5;
    public static int starting_spd_cost = 5;
    public static int starting_health_cost = 5;

    public static string support;
    public static string secondaryFire;
    public static string mobility;


    void start(){
        total_xp = starting_xp;
        atk_cost = starting_atk_cost;
        atkSpd_cost = starting_atkSpd_cost;
        jmp_cost = starting_jmp_cost;
        spd_cost = starting_jmp_cost;
        health_cost = starting_health_cost;
    }
    
}
