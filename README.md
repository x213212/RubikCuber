
![](https://i.imgur.com/iACkwzw.png)



#  How to use Ninja Code finish Project : RubikCube
Hi i am x213212,I have n’t been a ninja for a long time

so.... let begin Ninja training?

video:[yotuube](https://www.youtube.com/watch?v=djrOhimBbyQ)  project:[download](https://mega.nz/#F!cxt1HAoS!9hqsb6Xjy2H5SJNFQnZMbw?ohs0wCYQ)

# Ninja Code Example
![](https://i.imgur.com/oPPlqjG.png)

level:easy

# How to use Variable Naming correctly like Ninja?
```c#
        Debug.Log("now" + groupIndex + "\n");
        int[] groupIndexs = null;
        int[] groupIndexs2 = null;
        int[] groupIndexs3= null;
        int[] groupIndexs4 = null;

```

# How to use Expression Naming correctly like Ninja?
```c#
     if ((cube_arr[0, 7] == 0 && cube_arr[2, 7] == 2) || (cube_arr[0, 7] == 2 && cube_arr[2, 7] == 0) ||
            (cube_arr[1, 5] == 0 && cube_arr[3, 5] == 2) || (cube_arr[1, 5] == 2 && cube_arr[3, 5] == 0))
    else if ((cube_arr[0, 7] == 1 && cube_arr[2, 7] == 3) || (cube_arr[0, 7] == 3 && cube_arr[2, 7] == 1) ||
           (cube_arr[1, 5] == 1 && cube_arr[3, 5] == 3) || (cube_arr[1, 5] == 3 && cube_arr[3, 5] == 1))

```


# How to use Parameter correctly like Ninja?
```c#
public int checker2(int tmp, int tmp2, int tmp3, int tmp4, int tmp5, int tmp6, int color, int color2, int color3)
    {
      //  Debug.Log(cube_arr[tmp, tmp2] + "" + cube_arr[tmp3, tmp4] + "" + cube_arr[tmp5, tmp6]);
        if (cube_arr[tmp, tmp2] == color && cube_arr[tmp3, tmp4] == color2 && cube_arr[tmp5, tmp6] == color3)
        {
            cube_angle_colortrue = true;

            return 1;
        }
        if (cube_arr[tmp, tmp2] == color && cube_arr[tmp3, tmp4] == color3 && cube_arr[tmp5, tmp6] == color2)
        {
            cube_angle_colortrue = false;
            return 1;
        }
        if (cube_arr[tmp, tmp2] == color2 && cube_arr[tmp3, tmp4] == color3 && cube_arr[tmp5, tmp6] == color)
        {
            cube_angle_colortrue = false;
            return 1;
        }
        if (cube_arr[tmp, tmp2] == color2 && cube_arr[tmp3, tmp4] == color && cube_arr[tmp5, tmp6] == color3)
        {
            cube_angle_colortrue = false;
            return 1;
        }
        if (cube_arr[tmp, tmp2] == color3 && cube_arr[tmp3, tmp4] == color && cube_arr[tmp5, tmp6] == color2)
        {
            cube_angle_colortrue = false;
            return 1;
        }
        if (cube_arr[tmp, tmp2] == color3 && cube_arr[tmp3, tmp4] == color2 && cube_arr[tmp5, tmp6] == color)
        {
            cube_angle_colortrue = false;
            return 1;
        }
        else
        {
            return 0;
        }
    }
```
# How to use Debug correctly like Ninja?
```c#
  /*
        //Debug.Log(tmpstr);
        string tmpstr2 = "";
        if (main == 0)
        {
            //5341
            //1234
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[fif, i];
        }
        if (main == 1)
        {
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        if (main == 2)
        {
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[fif, i];
        }
        if (main == 3)
        {
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        if (main == 4)
        {
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        if (main == 5)
        {
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        else if (main == 6)
        {
            for (int i = 3; i < 6; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 1; i < 8; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 3; i < 6; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 1; i < 8; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        Debug.Log(tmpstr2);
        */
```


# How to use Return correctly  like Ninja?

![](https://i.imgur.com/coDIq3R.png)

level:medium

```c#
  public int findblock3(int color, int color2, int face)
    {
     //   Debug.Log("幾面" + groupIndex);



        if (checker(0, 1, 5, 1, color, color2, 0) == 1) { return 3; }
        if (checker(0, 3, 3, 1, color, color2, 0) == 1) { return 2; }
        if (checker(0, 7, 4, 1, color, color2, 0) == 1) { return 1; }
        if (checker(0, 5, 1, 1, color, color2, 0) == 1) { return 2; }

        if (checker(1, 1, 0, 5, color, color2, 0) == 1) { return 2; }
        if (checker(1, 3, 5, 5, color, color2, 0) == 1) { return 3; }
        if (checker(1, 5, 4, 5, color, color2, 0) == 1) { return 1; }
        if (checker(1, 7, 2, 5, color, color2, 0) == 1) { return 2; }

        if (checker(2, 1, 5, 7, color, color2, 0) == 1) { return 3; }
        if (checker(2, 5, 1, 7, color, color2, 0) == 1) { return 2; }
        if (checker(2, 3, 3, 7, color, color2, 0) == 1) { return 2; }
        if (checker(2, 7, 4, 7, color, color2, 0) == 1) { return 1; }

        if (checker(3, 1, 0, 3, color, color2, 0) == 1) { return 2; }
        if (checker(3, 3, 5, 3, color, color2, 0) == 1) { return 3; }
        if (checker(3, 5, 4, 3, color, color2, 0) == 1) { return 1; }
        if (checker(3, 7, 2, 3, color, color2, 0) == 1) { return 2; }



        if (checker(4, 1, 0, 7, color, color2, 0) == 1) { return 1; }
        if (checker(4, 3, 3, 5, color, color2, 0) == 1) { return 1; }
        if (checker(4, 5, 1, 5, color, color2, 0) == 1) { return 1; }
        if (checker(4, 7, 2, 7, color, color2, 0) == 1) { return 1; }

        if (checker(5, 1, 0, 1, color, color2, 0) == 1) { return 3; }
        if (checker(5, 3, 3, 3, color, color2, 0) == 1) { return 3; }
        if (checker(5, 5, 1, 3, color, color2, 0) == 1) { return 3; }
        if (checker(5, 7, 2, 1, color, color2, 0) == 1) { return 3; }

        return -1;
    }

```

# How to use Initialization correctly  like Ninja?

```c#
 cubes = new Transform[]
               {
            CreateCube(new int[] { cube_arr[0, 0] + 1, 0, cube_arr[5, 0] + 1, 0, cube_arr[3, 0] + 1, 0 }, new Vector3(-1, 1, -1)),
            CreateCube(new int[] { cube_arr[0, 1] + 1, 0, cube_arr[5, 1] + 1, 0, 0, 0 }, new Vector3(0, 1, -1)),
            CreateCube(new int[] { cube_arr[0, 2] + 1, 0, cube_arr[5, 2] + 1, 0, 0, cube_arr[1, 0] + 1}, new Vector3(1, 1, -1)),
            CreateCube(new int[] { cube_arr[0, 3] + 1, 0, 0, 0, cube_arr[3, 1] + 1, 1 }, new Vector3(-1, 0, -1)),
            CreateCube(new int[] { cube_arr[0, 4] + 1, 0, 0, 0, 0, 0 }, new Vector3(0, 0, -1)),
            CreateCube(new int[] { cube_arr[0, 5] + 1, 0, 0, 0, 0,  cube_arr[1, 1] + 1 }, new Vector3(1, 0, -1)),
            CreateCube(new int[] { cube_arr[0, 6] + 1, 0, 0, cube_arr[4, 0] + 1, cube_arr[3, 2] + 1,2 }, new Vector3(-1, -1, -1)),
            CreateCube(new int[] { cube_arr[0, 7] + 1, 0, 0, cube_arr[4, 1] + 1, 0, 0 }, new Vector3(0, -1, -1)),
            CreateCube(new int[] { cube_arr[0, 8] + 1, 0, 0, cube_arr[4, 2] + 1, 0,  cube_arr[1, 2] + 1 }, new Vector3(1, -1, -1)),
            CreateCube(new int[] { 0, 0, cube_arr[5, 3] + 1, 0, cube_arr[3, 3] + 1, 0 }, new Vector3(-1, 1, 0)),
            CreateCube(new int[] { 0, 0, cube_arr[5, 4] + 1, 0, 0, 0 }, new Vector3(0, 1, 0)),
            CreateCube(new int[] { 0, 0, cube_arr[5, 5] + 1, 0, 0,  cube_arr[1, 3] + 1  }, new Vector3(1, 1, 0)),
            CreateCube(new int[] { 0, 0, 0, 0, cube_arr[3, 4] + 1, 0 }, new Vector3(-1, 0, 0)),
            CreateCube(new int[] { 0, 0, 0, 0, 0, 0 }, new Vector3(0, 0, 0)),
            CreateCube(new int[] { 0, 0, 0, 0, 0,  cube_arr[1, 4] + 1  }, new Vector3(1, 0, 0)),
            CreateCube(new int[] { 0, 0, 0, cube_arr[4, 3] + 1, cube_arr[3, 5] + 1, 0 }, new Vector3(-1, -1, 0)),
            CreateCube(new int[] { 0, 0, 0, cube_arr[4, 4] + 1, 0, 0 }, new Vector3(0, -1, 0)),
            CreateCube(new int[] { 0, 0, 0, cube_arr[4, 5] + 1, 0,  cube_arr[1, 5] + 1  }, new Vector3(1, -1, 0)),
            CreateCube(new int[] { 0, cube_arr[2, 0] + 1, cube_arr[5, 6] + 1, 0, cube_arr[3, 6] + 1, 0 }, new Vector3(-1, 1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 1] + 1, cube_arr[5, 7] + 1, 0, 0, 0 }, new Vector3(0, 1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 2] + 1, cube_arr[5, 8] + 1, 0, 0, cube_arr[1, 6] + 1  }, new Vector3(1, 1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 3] + 1, 0, 0, cube_arr[3, 7] + 1, 0 }, new Vector3(-1, 0, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 4] + 1, 0, 0, 0, 0 }, new Vector3(0, 0, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 5] + 1, 0, 0, 0,cube_arr[1, 7] + 1  }, new Vector3(1, 0, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 6] + 1, 0, cube_arr[4, 6] + 1, cube_arr[3, 8] + 1, 0 }, new Vector3(-1, -1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 7] + 1, 0, cube_arr[4, 7] + 1, 0, 0 }, new Vector3(0, -1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 8] + 1, 0, cube_arr[4, 8] + 1, 0, cube_arr[1, 8] + 1  }, new Vector3(1, -1, 1)),
};
```

# How to use Call method like Ninja?

![](https://i.imgur.com/lYRJPHS.png)

level:high

# How to use Call method like Ninja?
        Cross(0, 5, 1, 7, 1, 1, 0, 5);
        Cross(1, 5, 3, 5, 5, 5, 5, 1);
        Cross(2, 5, 1, 7, 7, 7, 1, 4);
        Cross(3, 5, 3, 5, 3, 3, 4, 0);
        while (Cross(0, 5, 1, 7, 1, 1, 0, 5) == false) ;
        while (Cross(1, 5, 3, 5, 5, 5, 5, 1) == false) ;
        while (Cross(2, 5, 1, 7, 7, 7, 1, 4) == false) ;
        while (Cross(3, 5, 3, 5, 3, 3, 4, 0) == false) ;

# How to use Recursion correctly  like Ninja?

```c#

    public bool Cross(int first, int sec, int first_arrnumber, int first_arrnumber_2
        , int sec_arrnumber, int under, int face, int next)
    {      
        if (findblock3(first, sec, -1) == 3 && (checker(first, first_arrnumber, sec, sec_arrnumber, first, sec, 0)) == 1 && color_true == true)
            return true;
        if (findblock3(first, sec, -1) == 1)
        {
            while ((checker(first, first_arrnumber_2, 4, under, first, sec, 0)) == 0)
            {
                StartCoroutine(Move(3, true, true, false));
                ans_count++;
            }
            if ((checker(first, first_arrnumber_2, 4, under, first, sec, 0)) == 1 && color_true == false)
            {
                StartCoroutine(Move(face, false, true, false));
                StartCoroutine(Move(next, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(next, true, true, false));

                ans_count += 4;
            }
            else if ((checker(first, first_arrnumber_2, 4, under, first, sec, 0)) == 1 && color_true == true)
            {
                while ((checker(first, first_arrnumber, sec, sec_arrnumber, first, sec, 0)) == 0)
                {
                    StartCoroutine(Move(face, true, true, false));
                    ans_count++;
                }
            }
        }
        if (findblock3(first, sec, -1) == 2)
        {
            int tmp = find_blockwho(first, sec);
            StartCoroutine(Move(tmp, false, true, false));
            StartCoroutine(Move(3, true, true, false));
            StartCoroutine(Move(tmp, true, true, false));
            ans_count += 3;
        }
        if (findblock3(first, sec, -1) == 3)
        {
            while ((checker(first, first_arrnumber, sec, sec_arrnumber, first, sec, 0)) == 0)
            {
                StartCoroutine(Move(2, true, true, false));
                ans_count++;
            }
            if ((checker(first, first_arrnumber, sec, sec_arrnumber, first, sec, 0)) == 1 && color_true == false)
            {

                StartCoroutine(Move(face, false, true, false));

                ans_count++;
            }
        }

        Cross(first, sec, first_arrnumber, first_arrnumber_2,
            sec_arrnumber, under, face, next);
        return false;
    }
    
    
```
![](https://i.imgur.com/BCA13OI.png)

level:god

# How to use Calculation correctly like Ninja?

```c#
	/* 抓4角
		Circle(tmp,x1,y1, 50,new Color32(255, 0,0, 255));
		Circle(tmp,x2,y2, 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1,y2, 50,new Color32(255, 0,0, 255));
		Circle(tmp,x2,y1, 50,new Color32(255, 0,0, 255));
				Circle(tmp,x1+(((x2-x1)/4)*1),y1+(((y2-y1)/4)*1), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*2),y1+(((y2-y1)/4)*1), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*3),y1+(((y2-y1)/4)*1), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*1),y1+(((y2-y1)/4)*2), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*2),y1+(((y2-y1)/4)*2), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*3),y1+(((y2-y1)/4)*2), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*1),y1+(((y2-y1)/4)*3), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*2),y1+(((y2-y1)/4)*3), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*3),y1+(((y2-y1)/4)*3), 50,new Color32(255, 0,0, 255));
		*/
int test1=((((x2-x1)/4)*1))/2;
		int test2=((((y2-y1)/4)*1))/2;

        //int test2=;
        Circle(t,-test1+x1+(((x2-x1)/4)*1),-test2+y1+(((y2-y1)/4)*1), 30, GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), -test2 + y1 + (((y2 - y1) / 4) * 1),0));
		Circle(t,x1+(((x2-x1)/4)*2),-test2+y1+(((y2-y1)/4)*1), 30, GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), -test2 + y1 + (((y2 - y1) / 4) * 1), 0));
        Circle(t,test1+x1+(((x2-x1)/4)*3),-test2+y1+(((y2-y1)/4)*1), 30, GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), -test2 + y1 + (((y2 - y1) / 4) * 1), 0));

        Circle(t, -test1 + x1 + (((x2 - x1) / 4) * 1), y1 + (((y2 - y1) / 4) * 2), 30, GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), y1 + (((y2 - y1) / 4) * 2), 0));
        Circle(t,x1+(((x2-x1)/4)*2),y1+(((y2-y1)/4)*2), 30, GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), y1 + (((y2 - y1) / 4) * 2), 0));
        Circle(t,test1+x1+(((x2-x1)/4)*3),y1+(((y2-y1)/4)*2), 30, GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), y1 + (((y2 - y1) / 4) * 2), 0));

        Circle(t,-test1+x1+(((x2-x1)/4)*1),test2+y1+(((y2-y1)/4)*3), 30, GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), test2 + y1 + (((y2 - y1) / 4) * 3), 0));
        Circle(t,x1+(((x2-x1)/4)*2),test2+y1+(((y2-y1)/4)*3), 30, GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), test2 + y1 + (((y2 - y1) / 4) * 3), 0));
        Circle(t,test1+x1+(((x2-x1)/4)*3),test2+y1+(((y2-y1)/4)*3), 30, GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), test2 + y1 + (((y2 - y1) / 4) * 3), 0));

      Set_color("input1", GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), -test2 + y1 + (((y2 - y1) / 4) * 1), 1),0);
        Set_color("input2", GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), y1 + (((y2 - y1) / 4) * 2), 1), 1);
        Set_color("input3", GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), test2 + y1 + (((y2 - y1) / 4) * 3), 1), 2);


        Set_color("input4", GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), -test2 + y1 + (((y2 - y1) / 4) * 1), 1), 3);
        Set_color("input5", GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), y1 + (((y2 - y1) / 4) * 2), 1), 4);
        Set_color("input6", GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), test2 + y1 + (((y2 - y1) / 4) * 3), 1), 5);


        Set_color("input7", GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), -test2 + y1 + (((y2 - y1) / 4) * 1), 1), 6);
        Set_color("input8", GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), y1 + (((y2 - y1) / 4) * 2), 1), 7);
        Set_color("input9", GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), test2 + y1 + (((y2 - y1) / 4) * 3), 1), 8);
```


![](https://i.imgur.com/Li2lIu9.png)

level:?
!! only last function have comment!!
# How to use finish correctly like Ninja?
```c#
   public void findans()
    {

        history.Clear();
        history2.Clear();

        controlLock = true;
        int ans_count = 0;
        restart_cube =true;

        copy_arr(cube_arr, cube_arr2);
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 9; j++)
                if (i == cube_arr[i, j])
                    ans_count += 1;


        DateTime time_start = DateTime.Now;


        if (ans_count != 54)
        {

            // find_angle1(0, 1, 5);
            // Debug.Log("level" +findblock4(0, 1, 5));

            countText.text = (ans_count).ToString();
            show_all();
 
            ///第三層十字
            defaults= history.Count;
            copy_arr(cube_arr, defaultarr);

            Cross(0, 5, 1, 7, 1, 1, 0, 5);
            Cross(1, 5, 3, 5, 5, 5, 5, 1);
            Cross(2, 5, 1, 7, 7, 7, 1, 4);
            Cross(3, 5, 3, 5, 3, 3, 4, 0);
            while (Cross(0, 5, 1, 7, 1, 1, 0, 5) == false) ;
            while (Cross(1, 5, 3, 5, 5, 5, 5, 1) == false) ;
            while (Cross(2, 5, 1, 7, 7, 7, 1, 4) == false) ;
            while (Cross(3, 5, 3, 5, 3, 3, 4, 0) == false) ;
            topcross = history.Count;
            copy_arr(cube_arr, topcrossarr);

            ///第三層角塊
            while (cube_arr[0, 4] != 0)
                StartCoroutine(Move(6, true, true, false));
            find_angle1(0, 1, 5);
            StartCoroutine(Move(2, false, true, false));
            find_angle1(3, 0, 5);
            StartCoroutine(Move(2, false, true, false));
            find_angle1(2, 3, 5);
            StartCoroutine(Move(2, false, true, false));
            find_angle1(1, 2, 5);
            StartCoroutine(Move(2, false, true, false));
            topangle = history.Count;
            copy_arr(cube_arr, topanglearr);
            //第二層邊塊
            sec_block_level2(0, 5, 1, 1, 0, 1);
            sec_block_level2(0, 5, 1, 1, 3, 0);
            sec_block_level2(0, 5, 1, 1, 2, 3);
            sec_block_level2(0, 5, 1, 1, 1, 2);
            //第二層邊塊歸位
            
            while (cube_arr[0, 4] != 0)
                StartCoroutine(Move(6, true, true, false));
            midblock = history.Count;
            copy_arr(cube_arr, midblockarr);
            //第一層十字
            find_block3_level3(0, 4, 0);
            find_block3_level3(1, 4, 1);
            find_block3_level3(2, 4, 3);
            find_block3_level3(3, 4, 2);
  
            //第一層角塊
            fix_block3_level3();
            fix2_block3_level3();
 
            check_five();

            downcross = history.Count;
            copy_arr(cube_arr, downcrossarr);

            //五角歸位
            find_angle2(0, 1, 4);
            StartCoroutine(Move(3, false, true, false));
            find_angle2(1, 2, 4);
            StartCoroutine(Move(3, false, true, false));
            find_angle2(2, 3, 4);
            StartCoroutine(Move(3, false, true, false));
            find_angle2(3, 0, 4);
            StartCoroutine(Move(3, false, true, false));

        //  Debug.Log(final_angle);

            while (final_angle != 0)
            {
                StartCoroutine(Move(2, false, true, false));
                StartCoroutine(Move(0, false, true, false));
                StartCoroutine(Move(2, true, true, false));
                StartCoroutine(Move(0, true, true, false));
                plus_check();
            }

            final_check();
            downangle = history.Count;
            copy_arr(cube_arr, downanglearr);
            ans_tmp = (int[])history.ToArray();
        }
```

Hope i can help you become a ninja!

![](https://i.imgur.com/ZeVN2dU.png)

more Ninja code?

https://x8795278.blogspot.com/

![](https://i.imgur.com/7qXEwHZ.png)

https://www.youtube.com/watch?v=oUmTJfvO1ag

![](https://i.imgur.com/OVMROpa.png)

https://youtu.be/oUmTJfvO1ag
goood nigjit!
