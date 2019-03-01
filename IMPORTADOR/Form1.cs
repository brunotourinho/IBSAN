using IMPORTADOR.Models;
using IMPORTADOR.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMPORTADOR
{
    public partial class Form1 : Form
    {
        private readonly DapperRepository _repoDapper = new DapperRepository();
        private Stopwatch _watch = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnImportar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFileName.Text) && !string.IsNullOrEmpty(ddlTipoArquivo.Text))
            {
                //->
                Application.UseWaitCursor = true;
                btnImportar.Enabled = false;
                _watch.Reset();

                switch (ddlTipoArquivo.Text)
                {
                    case "IND_AG":
                        await IMPORTAR_IND_AG(txtFileName.Text);
                        break;

                    case "IND_ES":
                        await IMPORTAR_IND_ES(txtFileName.Text);
                        break;

                    case "IND_FIN":
                        await IMPORTAR_IND_FIN(txtFileName.Text);
                        break;

                    case "IND_QD":
                        await IMPORTAR_IND_QD(txtFileName.Text);
                        break;

                    case "INF_AG":
                        await IMPORTAR_INF_AG(txtFileName.Text);
                        break;

                    case "INF_ES":
                        await IMPORTAR_INF_ES(txtFileName.Text);
                        break;

                    case "INF_FN":
                        await IMPORTAR_INF_FN(txtFileName.Text);
                        break;

                    case "INF_GE":
                        await IMPORTAR_INF_GE(txtFileName.Text);
                        break;

                    case "INF_QD":
                        await IMPORTAR_INF_QD(txtFileName.Text);
                        break;
                    case "PRESTADORES":
                        await IMPORTAR_PRESTADORES(txtFileName.Text);
                        break;
                    case "IND_AE_AG":
                        await IMPORTAR_IND_AE_AG(txtFileName.Text);
                        break;
                    case "IND_AE_ES":
                        await IMPORTAR_IND_AE_ES(txtFileName.Text);
                        break;

                    case "INF_AE_AG":
                        await IMPORTAR_INF_AE_AG(txtFileName.Text);
                        break;

                    case "INF_AE_ES":
                        await IMPORTAR_INF_AE_ES(txtFileName.Text);
                        break;
                }

                //->
                Application.UseWaitCursor = false;
                btnImportar.Enabled = true;
                txtFileName.Clear();
            }
            else
            {
                MessageBox.Show("Selecione o Arquivo, Tipo de Arquivo e Layout a ser importado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFileName_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                txtFileName.Text = openFileDialog.FileName;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] arquivos = (string[])e.Data.GetData(DataFormats.FileDrop);
            openFileDialog.FileName = arquivos.First();
            txtFileName.Text = openFileDialog.FileName;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        private static decimal ToDecimal(string strValor)
        {
            if (strValor.Trim().Length > 0)
                return decimal.Parse(strValor);
            else
                return 0;
        }

        private async Task IMPORTAR_IND_AG(string fileName)
        {
            var cs = new List<IBSANBR_IND_AG>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_IND_AG()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Municipio = fragmentos[1].Trim(),
                            Estado = fragmentos[2].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            CodigoPrestador = fragmentos[4].Trim(),
                            Prestador = fragmentos[5].Trim(),
                            SiglaPrestador = fragmentos[6].Trim(),
                            Abrangencia = fragmentos[7].Trim(),
                            TipoServico = fragmentos[8].Trim(),
                            NaturezaJuridica = fragmentos[9],
                            IN001 = ToDecimal(fragmentos[10]),
                            IN009 = ToDecimal(fragmentos[11]),
                            IN010 = ToDecimal(fragmentos[12]),
                            IN011 = ToDecimal(fragmentos[13]),
                            IN013 = ToDecimal(fragmentos[14]),
                            IN014 = ToDecimal(fragmentos[15]),
                            IN017 = ToDecimal(fragmentos[16]),
                            IN020 = ToDecimal(fragmentos[17]),
                            IN022 = ToDecimal(fragmentos[18]),
                            IN023 = ToDecimal(fragmentos[19]),
                            IN025 = ToDecimal(fragmentos[20]),
                            IN028 = ToDecimal(fragmentos[21]),
                            IN043 = ToDecimal(fragmentos[22]),
                            IN044 = ToDecimal(fragmentos[23]),
                            IN049 = ToDecimal(fragmentos[24]),
                            IN050 = ToDecimal(fragmentos[25]),
                            IN051 = ToDecimal(fragmentos[26]),
                            IN052 = ToDecimal(fragmentos[27]),
                            IN053 = ToDecimal(fragmentos[28]),
                            IN055 = ToDecimal(fragmentos[29]),
                            IN057 = ToDecimal(fragmentos[30]),
                            IN058 = ToDecimal(fragmentos[31])
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_IND_AG(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task IMPORTAR_IND_AE_AG(string fileName)
        {
            var cs = new List<IBSANBR_IND_AE_AG>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_IND_AE_AG()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            IN001 = ToDecimal(fragmentos[6]),
                            IN009 = ToDecimal(fragmentos[7]),
                            IN010 = ToDecimal(fragmentos[8]),
                            IN011 = ToDecimal(fragmentos[9]),
                            IN013 = ToDecimal(fragmentos[10]),
                            IN014 = ToDecimal(fragmentos[11]),
                            IN017 = ToDecimal(fragmentos[12]),
                            IN020 = ToDecimal(fragmentos[13]),
                            IN022 = ToDecimal(fragmentos[14]),
                            IN023 = ToDecimal(fragmentos[15]),
                            IN025 = ToDecimal(fragmentos[16]),
                            IN028 = ToDecimal(fragmentos[17]),
                            IN043 = ToDecimal(fragmentos[18]),
                            IN044 = ToDecimal(fragmentos[19]),
                            IN049 = ToDecimal(fragmentos[20]),
                            IN050 = ToDecimal(fragmentos[21]),
                            IN051 = ToDecimal(fragmentos[22]),
                            IN052 = ToDecimal(fragmentos[23]),
                            IN053 = ToDecimal(fragmentos[24]),
                            IN055 = ToDecimal(fragmentos[25]),
                            IN057 = ToDecimal(fragmentos[26]),
                            IN058 = ToDecimal(fragmentos[27])
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_IND_AE_AG(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task IMPORTAR_IND_ES(string fileName)
        {
            var cs = new List<IBSANBR_IND_ES>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_IND_ES()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Municipio = fragmentos[1].Trim(),
                            Estado = fragmentos[2].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            CodigoPrestador = fragmentos[4].Trim(),
                            Prestador = fragmentos[5].Trim(),
                            SiglaPrestador = fragmentos[6].Trim(),
                            Abrangencia = fragmentos[7].Trim(),
                            TipoServico = fragmentos[8].Trim(),
                            NaturezaJuridica = fragmentos[9],
                            IN016 = ToDecimal(fragmentos[10]),
                            IN021 = ToDecimal(fragmentos[11]),
                            IN024 = ToDecimal(fragmentos[12]),
                            IN046 = ToDecimal(fragmentos[13]),
                            IN047 = ToDecimal(fragmentos[14]),
                            IN056 = ToDecimal(fragmentos[15]),
                            IN059 = ToDecimal(fragmentos[16]),
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_IND_ES(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task IMPORTAR_IND_AE_ES(string fileName)
        {
            var cs = new List<IBSANBR_IND_AE_ES>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_IND_AE_ES()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),                           
                            Referencia = fragmentos[3].Trim(),
                            IN015 = ToDecimal(fragmentos[6]),
                            IN016 = ToDecimal(fragmentos[7]),
                            IN021 = ToDecimal(fragmentos[8]),
                            IN024 = ToDecimal(fragmentos[9]),
                            IN046 = ToDecimal(fragmentos[10]),
                            IN047 = ToDecimal(fragmentos[11]),
                            IN056 = ToDecimal(fragmentos[12]),
                            IN059 = ToDecimal(fragmentos[13]),
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_IND_AE_ES(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task IMPORTAR_IND_FIN(string fileName)
        {
            var cs = new List<IBSANBR_IND_FIN>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_IND_FIN()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Municipio = fragmentos[1].Trim(),
                            Estado = fragmentos[2].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            CodigoPrestador = fragmentos[4].Trim(),
                            Prestador = fragmentos[5].Trim(),
                            SiglaPrestador = fragmentos[6].Trim(),
                            Abrangencia = fragmentos[7].Trim(),
                            TipoServico = fragmentos[8].Trim(),
                            NaturezaJuridica = fragmentos[9],
                            IN002 = ToDecimal(fragmentos[10]),
                            IN003 = ToDecimal(fragmentos[11]),
                            IN004 = ToDecimal(fragmentos[12]),
                            IN005 = ToDecimal(fragmentos[13]),
                            IN006 = ToDecimal(fragmentos[14]),
                            IN007 = ToDecimal(fragmentos[15]),
                            IN008 = ToDecimal(fragmentos[16]),
                            IN012 = ToDecimal(fragmentos[17]),
                            IN018 = ToDecimal(fragmentos[18]),
                            IN019 = ToDecimal(fragmentos[19]),
                            IN026 = ToDecimal(fragmentos[20]),
                            IN027 = ToDecimal(fragmentos[21]),
                            IN029 = ToDecimal(fragmentos[22]),
                            IN030 = ToDecimal(fragmentos[23]),
                            IN031 = ToDecimal(fragmentos[24]),
                            IN032 = ToDecimal(fragmentos[25]),
                            IN033 = ToDecimal(fragmentos[26]),
                            IN034 = ToDecimal(fragmentos[27]),
                            IN035 = ToDecimal(fragmentos[28]),
                            IN036 = ToDecimal(fragmentos[29]),
                            IN037 = ToDecimal(fragmentos[30]),
                            IN038 = ToDecimal(fragmentos[31]),
                            IN039 = ToDecimal(fragmentos[32]),
                            IN040 = ToDecimal(fragmentos[33]),
                            IN041 = ToDecimal(fragmentos[34]),
                            IN042 = ToDecimal(fragmentos[35]),
                            IN045 = ToDecimal(fragmentos[36]),
                            IN048 = ToDecimal(fragmentos[37]),
                            IN054 = ToDecimal(fragmentos[38]),
                            IN060 = ToDecimal(fragmentos[39]),
                            IN101 = ToDecimal(fragmentos[40]),
                            IN102 = ToDecimal(fragmentos[41])
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_IND_FIN(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task IMPORTAR_IND_QD(string fileName)
        {
            var cs = new List<IBSANBR_IND_QD>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.Unicode, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_IND_QD()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Municipio = fragmentos[1].Trim(),
                            Estado = fragmentos[2].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            CodigoPrestador = fragmentos[4].Trim(),
                            Prestador = fragmentos[5].Trim(),
                            SiglaPrestador = fragmentos[6].Trim(),
                            Abrangencia = fragmentos[7].Trim(),
                            TipoServico = fragmentos[8].Trim(),
                            NaturezaJuridica = fragmentos[9],
                            IN071 = ToDecimal(fragmentos[10]),
                            IN072 = ToDecimal(fragmentos[11]),
                            IN073 = ToDecimal(fragmentos[12]),
                            IN074 = ToDecimal(fragmentos[13]),
                            IN075 = ToDecimal(fragmentos[14]),
                            IN076 = ToDecimal(fragmentos[15]),
                            IN077 = ToDecimal(fragmentos[16]),
                            IN079 = ToDecimal(fragmentos[17]),
                            IN080 = ToDecimal(fragmentos[18]),
                            IN082 = ToDecimal(fragmentos[19]),
                            IN083 = ToDecimal(fragmentos[20]),
                            IN084 = ToDecimal(fragmentos[21]),
                            IN085 = ToDecimal(fragmentos[22])
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_IND_QD(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-> Informações

        private async Task IMPORTAR_INF_AG(string fileName)
        {
            var cs = new List<IBSANBR_INF_AG>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_INF_AG()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Municipio = fragmentos[1].Trim(),
                            Estado = fragmentos[2].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            CodigoPrestador = fragmentos[4].Trim(),
                            Prestador = fragmentos[5].Trim(),
                            SiglaPrestador = fragmentos[6].Trim(),
                            Abrangencia = fragmentos[7].Trim(),
                            TipoServico = fragmentos[8].Trim(),
                            NaturezaJuridica = fragmentos[9],
                            AG001 = ToDecimal(fragmentos[10]),
                            AG002 = ToDecimal(fragmentos[11]),
                            AG003 = ToDecimal(fragmentos[12]),
                            AG004 = ToDecimal(fragmentos[13]),
                            AG005 = ToDecimal(fragmentos[14]),
                            AG006 = ToDecimal(fragmentos[15]),
                            AG007 = ToDecimal(fragmentos[16]),
                            AG008 = ToDecimal(fragmentos[17]),
                            AG010 = ToDecimal(fragmentos[18]),
                            AG011 = ToDecimal(fragmentos[19]),
                            AG012 = ToDecimal(fragmentos[20]),
                            AG013 = ToDecimal(fragmentos[21]),
                            AG014 = ToDecimal(fragmentos[22]),
                            AG015 = ToDecimal(fragmentos[23]),
                            AG017 = ToDecimal(fragmentos[24]),
                            AG018 = ToDecimal(fragmentos[25]),
                            AG019 = ToDecimal(fragmentos[26]),
                            AG020 = ToDecimal(fragmentos[27]),
                            AG021 = ToDecimal(fragmentos[28]),
                            AG022 = ToDecimal(fragmentos[29]),
                            AG024 = ToDecimal(fragmentos[30]),
                            AG026 = ToDecimal(fragmentos[31]),
                            AG027 = ToDecimal(fragmentos[32]),
                            AG028 = ToDecimal(fragmentos[33]),
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_INF_AG(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task IMPORTAR_INF_AE_AG(string fileName)
        {
            var cs = new List<IBSANBR_INF_AE_AG>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_INF_AE_AG()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            AG001 = ToDecimal(fragmentos[6]),
                            AG002 = ToDecimal(fragmentos[7]),
                            AG003 = ToDecimal(fragmentos[8]),
                            AG004 = ToDecimal(fragmentos[9]),
                            AG005 = ToDecimal(fragmentos[10]),
                            AG006 = ToDecimal(fragmentos[11]),
                            AG007 = ToDecimal(fragmentos[12]),
                            AG008 = ToDecimal(fragmentos[13]),
                            AG010 = ToDecimal(fragmentos[14]),
                            AG011 = ToDecimal(fragmentos[15]),
                            AG012 = ToDecimal(fragmentos[16]),
                            AG013 = ToDecimal(fragmentos[17]),
                            AG014 = ToDecimal(fragmentos[18]),
                            AG015 = ToDecimal(fragmentos[19]),
                            AG017 = ToDecimal(fragmentos[20]),
                            AG018 = ToDecimal(fragmentos[21]),
                            AG019 = ToDecimal(fragmentos[22]),
                            AG020 = ToDecimal(fragmentos[23]),
                            AG021 = ToDecimal(fragmentos[24]),
                            AG022 = ToDecimal(fragmentos[25]),
                            AG024 = ToDecimal(fragmentos[26]),
                            AG026 = ToDecimal(fragmentos[27]),
                            AG027 = ToDecimal(fragmentos[28]),
                            AG028 = ToDecimal(fragmentos[29]),
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_INF_AE_AG(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task IMPORTAR_INF_ES(string fileName)
        {
            var cs = new List<IBSANBR_INF_ES>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_INF_ES()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Municipio = fragmentos[1].Trim(),
                            Estado = fragmentos[2].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            CodigoPrestador = fragmentos[4].Trim(),
                            Prestador = fragmentos[5].Trim(),
                            SiglaPrestador = fragmentos[6].Trim(),
                            Abrangencia = fragmentos[7].Trim(),
                            TipoServico = fragmentos[8].Trim(),
                            NaturezaJuridica = fragmentos[9],
                            ES001 = ToDecimal(fragmentos[10]),
                            ES002 = ToDecimal(fragmentos[11]),
                            ES003 = ToDecimal(fragmentos[12]),
                            ES004 = ToDecimal(fragmentos[13]),
                            ES005 = ToDecimal(fragmentos[14]),
                            ES006 = ToDecimal(fragmentos[15]),
                            ES007 = ToDecimal(fragmentos[16]),
                            ES008 = ToDecimal(fragmentos[17]),
                            ES009 = ToDecimal(fragmentos[18]),
                            ES012 = ToDecimal(fragmentos[19]),
                            ES013 = ToDecimal(fragmentos[20]),
                            ES014 = ToDecimal(fragmentos[21]),
                            ES015 = ToDecimal(fragmentos[22]),
                            ES026 = ToDecimal(fragmentos[23]),
                            ES028 = ToDecimal(fragmentos[24])
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_INF_ES(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task IMPORTAR_INF_AE_ES(string fileName)
        {
            var cs = new List<IBSANBR_INF_AE_ES>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_INF_AE_ES()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            ES001 = ToDecimal(fragmentos[6]),
                            ES002 = ToDecimal(fragmentos[7]),
                            ES003 = ToDecimal(fragmentos[8]),
                            ES004 = ToDecimal(fragmentos[9]),
                            ES005 = ToDecimal(fragmentos[10]),
                            ES006 = ToDecimal(fragmentos[11]),
                            ES007 = ToDecimal(fragmentos[12]),
                            ES008 = ToDecimal(fragmentos[13]),
                            ES009 = ToDecimal(fragmentos[14]),
                            ES012 = ToDecimal(fragmentos[15]),
                            ES013 = ToDecimal(fragmentos[16]),
                            ES014 = ToDecimal(fragmentos[17]),
                            ES015 = ToDecimal(fragmentos[18]),
                            ES026 = ToDecimal(fragmentos[19]),
                            ES028 = ToDecimal(fragmentos[20])
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_INF_AE_ES(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task IMPORTAR_INF_FN(string fileName)
        {
            var cs = new List<IBSANBR_INF_FN>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_INF_FN()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Municipio = fragmentos[1].Trim(),
                            Estado = fragmentos[2].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            CodigoPrestador = fragmentos[4].Trim(),
                            Prestador = fragmentos[5].Trim(),
                            SiglaPrestador = fragmentos[6].Trim(),
                            Abrangencia = fragmentos[7].Trim(),
                            TipoServico = fragmentos[8].Trim(),
                            NaturezaJuridica = fragmentos[9],
                            FN001 = ToDecimal(fragmentos[10]),
                            FN002 = ToDecimal(fragmentos[11]),
                            FN003 = ToDecimal(fragmentos[12]),
                            FN004 = ToDecimal(fragmentos[13]),
                            FN005 = ToDecimal(fragmentos[14]),
                            FN006 = ToDecimal(fragmentos[15]),
                            FN007 = ToDecimal(fragmentos[16]),
                            FN008 = ToDecimal(fragmentos[17]),
                            FN010 = ToDecimal(fragmentos[18]),
                            FN011 = ToDecimal(fragmentos[19]),
                            FN013 = ToDecimal(fragmentos[20]),
                            FN014 = ToDecimal(fragmentos[21]),
                            FN015 = ToDecimal(fragmentos[22]),
                            FN016 = ToDecimal(fragmentos[23]),
                            FN017 = ToDecimal(fragmentos[24]),
                            FN018 = ToDecimal(fragmentos[25]),
                            FN019 = ToDecimal(fragmentos[26]),
                            FN020 = ToDecimal(fragmentos[27]),
                            FN021 = ToDecimal(fragmentos[28]),
                            FN022 = ToDecimal(fragmentos[29]),
                            FN023 = ToDecimal(fragmentos[30]),
                            FN024 = ToDecimal(fragmentos[31]),
                            FN025 = ToDecimal(fragmentos[32]),
                            FN026 = ToDecimal(fragmentos[33]),
                            FN027 = ToDecimal(fragmentos[34]),
                            FN028 = ToDecimal(fragmentos[35]),
                            FN030 = ToDecimal(fragmentos[36]),
                            FN031 = ToDecimal(fragmentos[37]),
                            FN032 = ToDecimal(fragmentos[38]),
                            FN033 = ToDecimal(fragmentos[39]),
                            FN034 = ToDecimal(fragmentos[40]),
                            FN035 = ToDecimal(fragmentos[41]),
                            FN036 = ToDecimal(fragmentos[42]),
                            FN037 = ToDecimal(fragmentos[43]),
                            FN038 = ToDecimal(fragmentos[44]),
                            FN039 = ToDecimal(fragmentos[45]),
                            FN041 = ToDecimal(fragmentos[46]),
                            FN042 = ToDecimal(fragmentos[47]),
                            FN043 = ToDecimal(fragmentos[48]),
                            FN044 = ToDecimal(fragmentos[49]),
                            FN045 = ToDecimal(fragmentos[50]),
                            FN046 = ToDecimal(fragmentos[51]),
                            FN047 = ToDecimal(fragmentos[52]),
                            FN048 = ToDecimal(fragmentos[53]),
                            FN051 = ToDecimal(fragmentos[54]),
                            FN052 = ToDecimal(fragmentos[55]),
                            FN053 = ToDecimal(fragmentos[56]),
                            FN054 = ToDecimal(fragmentos[57]),
                            FN055 = ToDecimal(fragmentos[58]),
                            FN056 = ToDecimal(fragmentos[59]),
                            FN057 = ToDecimal(fragmentos[60]),
                            FN058 = ToDecimal(fragmentos[61])
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_INF_FN(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task IMPORTAR_INF_GE(string fileName)
        {
            var cs = new List<IBSANBR_INF_GE>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_INF_GE()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Municipio = fragmentos[1].Trim(),
                            Estado = fragmentos[2].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            CodigoPrestador = fragmentos[4].Trim(),
                            Prestador = fragmentos[5].Trim(),
                            SiglaPrestador = fragmentos[6].Trim(),
                            Abrangencia = fragmentos[7].Trim(),
                            TipoServico = fragmentos[8].Trim(),
                            NaturezaJuridica = fragmentos[9],
                            GE001 = ToDecimal(fragmentos[10]),
                            GE002 = ToDecimal(fragmentos[11]),
                            GE003 = ToDecimal(fragmentos[12]),
                            GE008 = ToDecimal(fragmentos[13]),
                            GE009 = ToDecimal(fragmentos[14]),
                            GE010 = ToDecimal(fragmentos[15]),
                            GE011 = ToDecimal(fragmentos[16]),
                            GE014 = ToDecimal(fragmentos[17]),
                            GE015 = ToDecimal(fragmentos[18]),
                            GE016 = ToDecimal(fragmentos[19]),
                            GE017 = ToDecimal(fragmentos[20]),
                            GE018 = ToDecimal(fragmentos[21]),
                            GE019 = fragmentos[22].Trim(),
                            GE020 = fragmentos[23].Trim(),
                            GE030 = ToDecimal(fragmentos[24]),
                            POP_TOT = ToDecimal(fragmentos[25]),
                            POP_URB = ToDecimal(fragmentos[26])
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_INF_GE(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task IMPORTAR_INF_QD(string fileName)
        {
            var cs = new List<IBSANBR_INF_QD>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new IBSANBR_INF_QD()
                        {
                            CodigoMunicipio = fragmentos[0].Trim(),
                            Municipio = fragmentos[1].Trim(),
                            Estado = fragmentos[2].Trim(),
                            Referencia = fragmentos[3].Trim(),
                            CodigoPrestador = fragmentos[4].Trim(),
                            Prestador = fragmentos[5].Trim(),
                            SiglaPrestador = fragmentos[6].Trim(),
                            Abrangencia = fragmentos[7].Trim(),
                            TipoServico = fragmentos[8].Trim(),
                            NaturezaJuridica = fragmentos[9],
                            QD001 = fragmentos[10].Trim(),
                            QD002 = ToDecimal(fragmentos[11]),
                            QD003 = ToDecimal(fragmentos[12]),
                            QD004 = ToDecimal(fragmentos[13]),
                            QD006 = ToDecimal(fragmentos[14]),
                            QD007 = ToDecimal(fragmentos[15]),
                            QD008 = ToDecimal(fragmentos[16]),
                            QD009 = ToDecimal(fragmentos[17]),
                            QD011 = ToDecimal(fragmentos[18]),
                            QD012 = ToDecimal(fragmentos[19]),
                            QD015 = ToDecimal(fragmentos[20]),
                            QD019 = ToDecimal(fragmentos[21]),
                            QD020 = ToDecimal(fragmentos[22]),
                            QD021 = ToDecimal(fragmentos[23]),
                            QD022 = ToDecimal(fragmentos[24]),
                            QD023 = ToDecimal(fragmentos[25]),
                            QD024 = ToDecimal(fragmentos[26]),
                            QD025 = ToDecimal(fragmentos[27]),
                            QD026 = ToDecimal(fragmentos[28]),
                            QD027 = ToDecimal(fragmentos[29]),
                            QD028 = ToDecimal(fragmentos[30])
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_IBSANBR_INF_QD(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-> Prestadores

        private async Task IMPORTAR_PRESTADORES(string fileName)
        {
            var cs = new List<Prestadores>();
            int lineCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    _watch.Start();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fragmentos = line.Split(';').ToList();

                        cs.Add(new Prestadores()
                        {
                            Referencia = fragmentos[0].Trim(),
                            CodigoMunicipio = fragmentos[1].Trim(),
                            CodigoPrestador = fragmentos[2].Trim(),
                            Prestador = fragmentos[3].Trim(),
                            Sigla = fragmentos[4].Trim(),
                            Abrangencia = fragmentos[5].Trim(),
                            TipoServico = fragmentos[6].Trim(),
                            Natureza = fragmentos[7],
                          
                        });
                        lineCount++;
                    }
                }

                try
                {
                    await _repoDapper.INSERT_PRESTADORES(cs);
                    _watch.Stop();
                    MessageBox.Show($"Arquivo importado com sucesso. { lineCount } registros importados\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _watch.Stop();
                    MessageBox.Show($"Erro ao gravar no Banco de Dados: { ex }\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _watch.Stop();
                MessageBox.Show($"Erro na linha {lineCount + 1}\n{ex.Message}\nTempo de execução: {_watch.Elapsed.ToString(@"hh\:mm\:ss")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}