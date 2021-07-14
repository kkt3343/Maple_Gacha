using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maplestroy_gacha
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //pictureBox1.ImageLocation = "images/rate.png";

            //배경
            //this.BackgroundImage = Properties.Resources.rate;
            //pictureBox1.BackgroundImage = Properties.Resources.rate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //강환불
            if (radioButton1.Checked == true)
            {
                nanana.ForeColor = Color.White;
                nanana.BackColor = Color.Red;
                nanana.Text = "강환불";
                영환불 kt = new 영환불();
                kt.set(true);
                option1.Text = kt.giveoption[0];
                option2.Text = kt.giveoption[1];
                option3.Text = kt.giveoption[2];
                option4.Text = kt.giveoption[3];
            }
            //영환불
            else if(radioButton2.Checked == true)
            {
                nanana.ForeColor = Color.White;
                nanana.BackColor = Color.Green;
                nanana.Text = "영환불";
                영환불 kt = new 영환불();
                kt.set(false);
                option1.Text = kt.giveoption[0];
                option2.Text = kt.giveoption[1];
                option3.Text = kt.giveoption[2];
                option4.Text = kt.giveoption[3];
            }
            //체크안함
            else
            {
                MessageBox.Show("어떠한 환불을 돌릴지 선택해 주세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    
    public class 영환불
    {
        public string[] giveoption = new string[4] { "옵1","옵2","옵3","옵4"};
        public struct box
        {
            //추옵의 이름
            public string name;
            //뽑는 확률
            public int rate;
            //만약에 true면 해당옵션은 더이상 뽑지 않는다.
            public bool check;
            public box(string name, int rate, bool check)
            {
                this.name = name;
                this.rate = rate;
                this.check = check;
            }
        };

        string weight(int value,bool a)
        {
            bool 강환불 = a;
            //강환불 확률 (0.xx 단위에도 적용할수 있도록 double로 확장)
            // 1추 2추 3추 4추
            // 20% 30% 36% 14%
            double[] gang = new double[4];
            gang[0] = 20;
            gang[1] = 30;
            gang[2] = 36;
            gang[3] = 14;
            //영환불 확률
            // 2추 3추 4추 5추
            // 29% 45% 25% 1%
            double[] young = new double[4];
            young[0] = 29;
            young[1] = 45;
            young[2] = 25;
            young[3] = 1;


            double[] boxrate = new double[4];
            if(강환불 == true)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        boxrate[i] = boxrate[i] + gang[j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        boxrate[i] = boxrate[i] + young[j];
                    }
                }
            }

            string temp = "";
            if (value > 0 && value <= boxrate[0])
            {
                if(강환불==true)
                {
                    temp = "1추";
                }
                else
                {
                    temp = "2추";
                }               
            }
            else if (value > boxrate[0] && value <= boxrate[1])
            {
                if (강환불 == true)
                {
                    temp = "2추";
                }
                else
                {
                    temp = "3추";
                }
            }
            else if (value > boxrate[1] && value <= boxrate[2])
            {
                if (강환불 == true)
                {
                    temp = "3추";
                }
                else
                {
                    temp = "4추";
                }
            }
            else if (value > boxrate[2] && value <= boxrate[3])
            {
                if (강환불 == true)
                {
                    temp = "4추";
                }
                else
                {
                    temp = "★★5추★★";
                }
            }
            return temp;
        }

        box[] boxes = new box[20];
        public void set(bool gangisture)
        {          
            //옵션설정
            //                  이름 확률 수정X
            boxes[0] = new box("STR", 2, false);
            boxes[1] = new box("DEX", 2, false);
            boxes[2] = new box("INT", 2, false);
            boxes[3] = new box("LUK", 2, false);
            boxes[4] = new box("STR+DEX", 4, false);
            boxes[5] = new box("STR+INT", 4, false);
            boxes[6] = new box("STR+LUK", 4, false);
            boxes[7] = new box("DEX+INT", 4, false);
            boxes[8] = new box("DEX+LUK", 4, false);
            boxes[9] = new box("INT+LUK", 4, false);
            boxes[10] = new box("최대 HP", 9, false);
            boxes[11] = new box("최대 MP", 10, false);
            boxes[12] = new box("착용레벨감소", 10, false);
            boxes[13] = new box("방어력", 10, false);
            boxes[14] = new box("공격력", 4, false);
            boxes[15] = new box("마력", 4, false);
            boxes[16] = new box("이동속도", 10, false);
            boxes[17] = new box("점프력", 10, false);
            boxes[18] = new box("올스탯%", 1, false);
            boxes[19] = new box("", 0, false);

            //지정된 확률에 따라 구간 설정
            int[] tem = new int[20];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    tem[i] = tem[i] + boxes[j].rate;
                }
            }

            //여기서부터 핵심코드
            int first = 0, front = 10, back = 50;
            //추옵은 4번 뽑으니까 4번 반복합니다.

            //랜덤값 설정
            Random r = new Random();

            for(int ppp = 0; ppp < 4; ppp++)
            {             
                //어떤 추옵을 뽑을지 결정(처음)
                if (ppp == 0)
                {
                    //★랜덤★//                  
                    first = r.Next(1, 101);

                    if (first > 0 && first <= tem[0] && boxes[0].check == false) //str
                    {
                        giveoption[ppp] = boxes[0].name;
                        boxes[0].check = true;
                        front = 1;
                        back = tem[0];
                    }
                    else if (first > tem[0] && first <= tem[1] && boxes[1].check == false) //dex
                    {
                        giveoption[ppp] = boxes[1].name;
                        boxes[1].check = true;
                        front = tem[0];
                        back = tem[1];
                    }
                    else if (first > tem[1] && first <= tem[2] && boxes[2].check == false) //int
                    {
                        giveoption[ppp] = boxes[2].name;
                        boxes[2].check = true;
                        front = tem[1];
                        back = tem[2];
                    }
                    else if (first > tem[2] && first <= tem[3] && boxes[3].check == false) //luk
                    {
                        giveoption[ppp] = boxes[3].name;
                        boxes[3].check = true;
                        front = tem[2];
                        back = tem[3];
                    }
                    else if (first > tem[3] && first <= tem[4] && boxes[4].check == false) //str+dex
                    {
                        giveoption[ppp] = boxes[4].name;
                        boxes[4].check = true;
                        front = tem[3];
                        back = tem[4];
                    }
                    else if (first > tem[4] && first <= tem[5] && boxes[5].check == false) //str+int
                    {
                        giveoption[ppp] = boxes[5].name;
                        boxes[5].check = true;
                        front = tem[4];
                        back = tem[5];
                    }
                    else if (first > tem[5] && first <= tem[6] && boxes[6].check == false) //str+luk
                    {
                        giveoption[ppp] = boxes[6].name;
                        boxes[6].check = true;
                        front = tem[5];
                        back = tem[6];
                    }
                    else if (first > tem[6] && first <= tem[7] && boxes[7].check == false) //dex+int
                    {
                        giveoption[ppp] = boxes[7].name;
                        boxes[7].check = true;
                        front = tem[6];
                        back = tem[7];
                    }
                    else if (first > tem[7] && first <= tem[8] && boxes[8].check == false) //dex+luk
                    {
                        giveoption[ppp] = boxes[8].name;
                        boxes[8].check = true;
                        front = tem[7];
                        back = tem[8];
                    }
                    else if (first > tem[8] && first <= tem[9] && boxes[9].check == false) //int+luk
                    {
                        giveoption[ppp] = boxes[9].name;
                        boxes[9].check = true;
                        front = tem[8];
                        back = tem[9];
                    }
                    else if (first > tem[9] && first <= tem[10] && boxes[10].check == false) //최대hp
                    {
                        giveoption[ppp] = boxes[10].name;
                        boxes[10].check = true;
                        front = tem[9];
                        back = tem[10];
                    }
                    else if (first > tem[10] && first <= tem[11] && boxes[11].check == false) //최대mp
                    {
                        giveoption[ppp] = boxes[11].name;
                        boxes[11].check = true;
                        front = tem[10];
                        back = tem[11];
                    }
                    else if (first > tem[11] && first <= tem[12] && boxes[12].check == false) //착감
                    {
                        giveoption[ppp] = boxes[12].name;
                        boxes[12].check = true;
                        front = tem[11];
                        back = tem[12];
                    }
                    else if (first > tem[12] && first <= tem[13] && boxes[13].check == false) //방어력
                    {
                        giveoption[ppp] = boxes[13].name;
                        boxes[13].check = true;
                        front = tem[12];
                        back = tem[13];
                    }
                    else if (first > tem[13] && first <= tem[14] && boxes[14].check == false) //공격력
                    {
                        giveoption[ppp] = boxes[14].name;
                        boxes[14].check = true;
                        front = tem[13];
                        back = tem[14];
                    }
                    else if (first > tem[14] && first <= tem[15] && boxes[15].check == false) //마력
                    {
                        giveoption[ppp] = boxes[15].name;
                        boxes[15].check = true;
                        front = tem[14];
                        back = tem[15];
                    }
                    else if (first > tem[15] && first <= tem[16] && boxes[16].check == false) //이동속도
                    {
                        giveoption[ppp] = boxes[16].name;
                        boxes[16].check = true;
                        front = tem[15];
                        back = tem[16];
                    }
                    else if (first > tem[16] && first <= tem[17] && boxes[17].check == false) //점프력
                    {
                        giveoption[ppp] = boxes[17].name;
                        boxes[17].check = true;
                        front = tem[16];
                        back = tem[17];
                    }
                    else if (first > tem[17] && first <= tem[18] && boxes[18].check == false) //올스탯%
                    {
                        giveoption[ppp] = boxes[18].name;
                        boxes[18].check = true;
                        front = tem[17];
                        back = tem[18];
                    }
                    else
                    {

                    }

                }
                //그외 (처음이 아닐 때)
                else
                {
                    int left = 0, right = 0;
                    //★랜덤★//
                    first = r.Next(1, front+1);
                    if (first > 0 && first <= tem[0] && boxes[0].check == false) //str
                    {
                        left = 0;
                    }
                    else if (first > tem[0] && first <= tem[1] && boxes[1].check == false) //dex
                    {
                        left = 1;
                    }
                    else if (first > tem[1] && first <= tem[2] && boxes[2].check == false) //int
                    {
                        left = 2;
                    }
                    else if (first > tem[2] && first <= tem[3] && boxes[3].check == false) //luk
                    {
                        left = 3;
                    }
                    else if (first > tem[3] && first <= tem[4] && boxes[4].check == false) //str+dex
                    {
                        left = 4;
                    }
                    else if (first > tem[4] && first <= tem[5] && boxes[5].check == false) //str+int
                    {
                        left = 5;
                    }
                    else if (first > tem[5] && first <= tem[6] && boxes[6].check == false) //str+luk
                    {
                        left = 6;
                    }
                    else if (first > tem[6] && first <= tem[7] && boxes[7].check == false) //dex+int
                    {
                        left = 7;
                    }
                    else if (first > tem[7] && first <= tem[8] && boxes[8].check == false) //dex+luk
                    {
                        left = 8;
                    }
                    else if (first > tem[8] && first <= tem[9] && boxes[9].check == false) //int+luk
                    {
                        left = 9;
                    }
                    else if (first > tem[9] && first <= tem[10] && boxes[10].check == false) //최대hp
                    {
                        left = 10;
                    }
                    else if (first > tem[10] && first <= tem[11] && boxes[11].check == false) //최대mp
                    {
                        left = 11;
                    }
                    else if (first > tem[11] && first <= tem[12] && boxes[12].check == false) //착감
                    {
                        left = 12;
                    }
                    else if (first > tem[12] && first <= tem[13] && boxes[13].check == false) //방어력
                    {
                        left = 13;
                    }
                    else if (first > tem[13] && first <= tem[14] && boxes[14].check == false) //공격력
                    {
                        left = 14;
                    }
                    else if (first > tem[14] && first <= tem[15] && boxes[15].check == false) //마력
                    {
                        left = 15;
                    }
                    else if (first > tem[15] && first <= tem[16] && boxes[16].check == false) //이동속도
                    {
                        left = 16;
                    }
                    else if (first > tem[16] && first <= tem[17] && boxes[17].check == false) //점프력
                    {
                        left = 17;
                    }
                    else if (first > tem[17] && first <= tem[18] && boxes[18].check == false) //올스탯%
                    {
                        left = 18;
                    }
                    else
                    {
                        left = 19;
                    }
                    //==============================================================================//

                    first = r.Next(back, 101);
                    if (first > 0 && first <= tem[0] && boxes[0].check == false) //str
                    {
                        right = 0;
                    }
                    else if (first > tem[0] && first <= tem[1] && boxes[1].check == false) //dex
                    {
                        right = 1;
                    }
                    else if (first > tem[1] && first <= tem[2] && boxes[2].check == false) //int
                    {
                        right = 2;
                    }
                    else if (first > tem[2] && first <= tem[3] && boxes[3].check == false) //luk
                    {
                        right = 3;
                    }
                    else if (first > tem[3] && first <= tem[4] && boxes[4].check == false) //str+dex
                    {
                        right = 4;
                    }
                    else if (first > tem[4] && first <= tem[5] && boxes[5].check == false) //str+int
                    {
                        right = 5;
                    }
                    else if (first > tem[5] && first <= tem[6] && boxes[6].check == false) //str+luk
                    {
                        right = 6;
                    }
                    else if (first > tem[6] && first <= tem[7] && boxes[7].check == false) //dex+int
                    {
                        right = 7;
                    }
                    else if (first > tem[7] && first <= tem[8] && boxes[8].check == false) //dex+luk
                    {
                        right = 8;
                    }
                    else if (first > tem[8] && first <= tem[9] && boxes[9].check == false) //int+luk
                    {
                        right = 9;
                    }
                    else if (first > tem[9] && first <= tem[10] && boxes[10].check == false) //최대hp
                    {
                        right = 10;
                    }
                    else if (first > tem[10] && first <= tem[11] && boxes[11].check == false) //최대mp
                    {
                        right = 11;
                    }
                    else if (first > tem[11] && first <= tem[12] && boxes[12].check == false) //착감
                    {
                        right = 12;
                    }
                    else if (first > tem[12] && first <= tem[13] && boxes[13].check == false) //방어력
                    {
                        right = 13;
                    }
                    else if (first > tem[13] && first <= tem[14] && boxes[14].check == false) //공격력
                    {
                        right = 14;
                    }
                    else if (first > tem[14] && first <= tem[15] && boxes[15].check == false) //마력
                    {
                        right = 15;
                    }
                    else if (first > tem[15] && first <= tem[16] && boxes[16].check == false) //이동속도
                    {
                        right = 16;
                    }
                    else if (first > tem[16] && first <= tem[17] && boxes[17].check == false) //점프력
                    {
                        right = 17;
                    }
                    else if (first > tem[17] && first <= tem[18] && boxes[18].check == false) //올스탯%
                    {
                        right = 18;
                    }
                    else
                    {
                        right = 19;
                    }

                    //만약에 둘다 miss 일때
                    if(left == 19 && right == 19)
                    {
                        ppp--;
                        continue;
                    }


                    //위에서 선택한 값중 하나 택
                    if (left == 19)
                    {
                        boxes[right].check = true;
                        giveoption[ppp] = boxes[right].name;
                        if (right == 18)
                        {
                            front = tem[right - 1];
                            back = 100;
                        }
                        else
                        {
                            front = tem[right - 1];
                            back = tem[right];
                        }
                    }
                    else if(right == 19)
                    {
                        boxes[left].check = true;
                        giveoption[ppp] = boxes[left].name;
                        if (left == 0)
                        {
                            front = 1;
                            back = tem[left];
                        }
                        else
                        {
                            front = tem[left - 1];
                            back = tem[left];
                        }
                    }
                    else
                    {
                        //위 아래 둘중 하나 결정                      
                        //★랜덤★//
                        int choose = r.Next(0, 2);
                        if(choose == 0)
                        {
                            giveoption[ppp] = boxes[left].name;
                            boxes[left].check = true;
                            if (left == 0)
                            {
                                front = 1;
                                back = tem[left];
                            }
                            else
                            {
                                front = tem[left - 1];
                                back = tem[left];
                            }
                        }
                        else
                        {
                            giveoption[ppp] = boxes[right].name;
                            boxes[right].check = true;
                            if (right == 18)
                            {
                                front = tem[right - 1];
                                back = 100;
                            }                          
                            else
                            {
                                front = tem[right - 1];
                                back = tem[right];
                            }
                        }
                    }
                }
                //추옵의 가중치(1~5추)
                //★랜덤★//
                int spsp = r.Next(1, 101);
                string temp = "null";

                //이거 true 면 강환불 아니면 영환불
                bool 강환불입니다 = gangisture;

                temp = weight(spsp,강환불입니다);

                //옵션을 박스에 집어넣는다.
                giveoption[ppp] = giveoption[ppp] + " ( " + temp + " ) ";
            }                 
        }
    }
}
