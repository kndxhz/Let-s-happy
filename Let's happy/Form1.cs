using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

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

            // ������ԱȨ��
            if (!IsRunningAsAdministrator())
            {
                MessageBox.Show("���Թ���Ա������б�����");
                Application.Exit();
            }
        }

        private static bool IsRunningAsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
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
            Task.Run(() =>
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string outputPath = Path.Combine(appDirectory, "ntsd.exe");

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

                Process[] processes = Process.GetProcessesByName("StudentMain");
                string pid = "";
                if (processes.Length > 0)
                {
                    foreach (Process process in processes)
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show($"������: {process.ProcessName}, PID: {process.Id}");
                        }));
                        pid = process.Id.ToString();
                    }
                }
                else
                {
                    this.Invoke(new Action(() => { MessageBox.Show("δ�ҵ��������"); }));
                    return;
                }

                string command = "ntsd.exe";
                string arguments = $"-c q -p {pid}";

                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command} {arguments}",
                    UseShellExecute = true,
                    Verb = "runas",
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                };

                try
                {
                    Process process = Process.Start(processStartInfo);
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() => { MessageBox.Show($"��������: {ex.Message}"); }));
                }
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string outputPath = Path.Combine(appDirectory, "sethc.exe");

                using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Let_s_happy.Resources.sethc.exe"))
                {
                    if (resourceStream != null)
                    {
                        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            resourceStream.CopyTo(fileStream);
                        }
                    }

                    var processInfo = new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        Arguments = "C:\\Windows\\System32",
                        Verb = "runas",
                        UseShellExecute = true,
                        CreateNoWindow = true
                    };

                    try
                    {
                        Process.Start(processInfo);
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() => { MessageBox.Show($"Error: {ex.Message}"); }));
                    }

                    this.Invoke(new Action(() => { MessageBox.Show("���ͷţ����ֶ��滻"); }));
                }
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Let_s_happy.Resources.sethc.exe"))
                {
                    if (resourceStream != null)
                    {
                        using (var fileStream = new FileStream("C:\\system.exe", FileMode.Create, FileAccess.Write))
                        {
                            resourceStream.CopyTo(fileStream);
                        }
                    }
                }
                this.Invoke(new Action(() => { MessageBox.Show("���ͷ���C�̸�Ŀ¼"); }));
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                DialogResult result = MessageBox.Show(
                    "�����������@ZiHaoSaMa66����\n��ֻ�������ں�\n��л��λ����\n����ǽ����������Ŀ��GitHub��ҳ",
                    "��ʾ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo("https://github.com/ZiHaoSaMa66/OsEasy-ToolBox/") { UseShellExecute = true });
                    return;
                }

                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string outputPath = Path.Combine(appDirectory, "ToolBox 1.7 RC.exe");

                using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Let_s_happy.Resources.ToolBox 1.7 RC.exe"))
                {
                    if (resourceStream != null)
                    {
                        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            resourceStream.CopyTo(fileStream);
                        }
                    }
                }

                outputPath = Path.Combine(appDirectory, "ScreenRender_Helper.exe");

                using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Let_s_happy.Resources.ScreenRender_Helper.exe"))
                {
                    if (resourceStream != null)
                    {
                        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            resourceStream.CopyTo(fileStream);
                        }
                    }
                }

                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "ToolBox 1.7 RC.exe",
                    UseShellExecute = true,
                    Verb = "runas",
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                };

                try
                {
                    Process process = Process.Start(processStartInfo);
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() => { MessageBox.Show($"��������: {ex.Message}"); }));
                }
            });
        }
    }
}
