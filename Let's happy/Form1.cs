using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Security.Principal;

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
            static bool IsRunningAsAdministrator()
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            if (IsRunningAsAdministrator() == false)
            {
                MessageBox.Show("���Թ���Ա������б�����");
                Application.Exit();
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            // ��ȡӦ�ó��������Ŀ¼
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string outputPath = Path.Combine(appDirectory, "ntsd.exe");

            // ����Դ�ļ�����Ϊ�����ļ�
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Let_s_happy.Resources.ntsd.exe"))
            {
                if (resourceStream != null)
                {
                    using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }


            }
            // ͨ����������ȡ����ƥ��Ľ���
            Process[] processes = Process.GetProcessesByName("StudentMain");
            string pid = "";
            if (processes.Length > 0)
            {
                foreach (Process process in processes)
                {
                    MessageBox.Show($"������: {process.ProcessName}, PID: {process.Id}");
                    pid = process.Id.ToString();
                }
            }
            else
            {
                MessageBox.Show("δ�ҵ��������");
                return;
            }
            // ����Ҫִ�е�����Ͳ���
            string command = "ntsd.exe"; // �滻ΪҪִ�е�����
            string arguments = $"-c q -p {pid}"; // �滻Ϊ����Ĳ���

            // ����һ���µĽ���
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe", // ʹ�� cmd.exe ִ������
                Arguments = $"/c {command} {arguments}", // /c ������ʾ��ִ���������ر������
                UseShellExecute = true, // ʹ��ϵͳ��ǳ�������
                Verb = "runas", // �Թ���Ա�������
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory // ���õ�ǰ����Ŀ¼Ϊ��������Ŀ¼
            };

            try
            {
                // ��������
                Process process = Process.Start(processStartInfo);
                process.WaitForExit(); // �ȴ�����ִ�����
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
