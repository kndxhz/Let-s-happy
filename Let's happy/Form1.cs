using System;
using System.Windows.Forms;

namespace Let_s_happy
{
    public partial class Form1 : Form
    {
        // ����ȫ�ֱ���
        string[] one_sentence = { "��ӭ������ҵѧУ��", 
            "ѧϰҪע�����ݽ��", 
            "��ȷ�����Ѿ������Ͽε�������ʹ�ñ����", 
            "����������Ƽ�������Ψһ����",
            "�������ñ��������������������"};
        int i = 0; // ���ڹ����ļ�����

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ��ʾ������Ϣ
            MessageBox.Show("�������ɿ��͵�С��ֽ����\n����kndxhz@163.com\n��л���ҵѧ��ʹ��",
                            "������Ϣ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            timer1.Start(); // ������ʱ��
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // ������ʾone_sentence������
            label1.Text = one_sentence[i]; // ����ǰone_sentence�е�ֵ��ʾ��label1��

            // ����i��ʹ��ָ����һ������
            i++;

            // ���i�������鳤�ȣ�������Ϊ0��ʵ��ѭ��
            if (i >= one_sentence.Length)
            {
                i = 0;
            }
        }
    }
}
