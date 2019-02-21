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
                    case "IBSANBR_IND_AG":
                        await IMPORTAR_IBSANBR_IND_AG(txtFileName.Text);
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

        private async Task IMPORTAR_IBSANBR_IND_AG(string fileName)
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
                            IN058 = ToDecimal(fragmentos[10]),
                            IN001 = ToDecimal(fragmentos[11]),
                            IN009 = ToDecimal(fragmentos[12]),
                            IN010 = ToDecimal(fragmentos[13]),
                            IN011 = ToDecimal(fragmentos[14]),
                            IN013 = ToDecimal(fragmentos[15]),
                            IN014 = ToDecimal(fragmentos[16]),
                            IN017 = ToDecimal(fragmentos[17]),
                            IN020 = ToDecimal(fragmentos[18]),
                            IN022 = ToDecimal(fragmentos[19]),
                            IN023 = ToDecimal(fragmentos[20]),
                            IN025 = ToDecimal(fragmentos[21]),
                            IN028 = ToDecimal(fragmentos[22]),
                            IN043 = ToDecimal(fragmentos[23]),
                            IN044 = ToDecimal(fragmentos[24]),
                            IN049 = ToDecimal(fragmentos[25]),
                            IN050 = ToDecimal(fragmentos[26]),
                            IN051 = ToDecimal(fragmentos[27]),
                            IN052 = ToDecimal(fragmentos[28]),
                            IN053 = ToDecimal(fragmentos[29]),
                            IN055 = ToDecimal(fragmentos[30]),
                            IN057 = ToDecimal(fragmentos[31])
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
    }
}