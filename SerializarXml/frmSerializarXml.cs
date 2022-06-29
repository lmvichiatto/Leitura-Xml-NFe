using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SerializarXml.Serializable;
using SerializarXml.ModelSerialization;
using System.Globalization;


namespace SerializarXml
{
    public partial class frmSerializarXml : Form
    {
        public frmSerializarXml()
        {
            InitializeComponent();
        }

        private void btnLerXml_Click(object sender, EventArgs e)
        {
            LerXml();
        }

        private void LerXml()
        {
            try
            {
                if (openFileXml.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtpathXml.Text = openFileXml.FileName;

                    NFeSerialization serializable = new NFeSerialization();
                    var nfe = serializable.GetObjectFromFile<NFeProc>(txtpathXml.Text);

                    if (nfe == null)
                    {
                        MessageBox.Show("Falha ao ler o arquivo xml. Verifique se o arquivo é de uma NF-e/NFC-e autorizada!", "Aviso - Leitura do Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        popularForm(nfe);
                        MessageBox.Show("Arquivo xml da Nota Fiscal lido com Sucesso!", "Aviso - Leitura do Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Falha no processo de leitura do arquivo xml da Nota Fiscal.", "Aviso - Leitura do Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void popularForm(NFeProc nfe)
        {
            /* Populando tab Identificação */
            txtNaturezaOperacao.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.natOp;
            txtNumero.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.nNF;
            txtModelo.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.mod;
            txtSerie.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.serie.ToString();
            txtDataEmissao.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.dhEmi.ToShortDateString();

            /* Populando tab Emitente */
            txtRazaoSocial.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Emitente.xNome;
            txtNomeFantasia.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Emitente.xFant;
            txtCpfCnpjEmitente.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Emitente.CNPJ;
            txtInscricaoEstadual.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Emitente.IE;
            txtLogradouroEmitente.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Emitente.Endereco.xLgr;
            txtNroEmitente.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Emitente.Endereco.nro;
            txtMunicipioEmitente.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Emitente.Endereco.xMun;
            txtUFEmitente.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Emitente.Endereco.UF;

            /* Populando tab Destinatário */
            txtDestNomeFantasia.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.xNome;
            txtDestCpfCnpj.Text = string.IsNullOrEmpty(nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.CNPJ) ? nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.CPF : nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.CNPJ;
            txtDestEmail.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.email;
            txtDestLogradouro.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.Endereco.xLgr;
            txtDestNumero.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.Endereco.nro;
            txtDestMunicipio.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.Endereco.xMun;
            txtDestUF.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.Endereco.UF;
            txtDestCEP.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.Endereco.CEP;
            txtDestBairro.Text = nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario.Endereco.xBairro;
            txtNFid.Text = nfe.NotaFiscalEletronica.InformacoesNFe.id_nfe;
            txtChave.Text = nfe.NFeChaveNota.InformacoesNFe.chNFe;
            txtNovoNrNFe.Text = (("88" + txtNumero.Text).Length > 9 ? ("88" + txtNumero.Text).Substring(0, 9) : ("88" + txtNumero.Text));
            txtNovaChaveNFe.Text = GeraNovaChave(txtChave.Text, txtDataEmissao.Text, txtNovoNrNFe.Text, txtSerie.Text);
            lstOutrasInf.Items.Clear();
            lstOutrasInf.Items.Add("-> " + nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.nNF);
            nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.nNF = txtNovoNrNFe.Text;
            lstOutrasInf.Items.Add("-> " + nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.nNF);

            lstOutrasInf.Items.Add("-> " + nfe.NFeChaveNota.InformacoesNFe.chNFe);
            nfe.NFeChaveNota.InformacoesNFe.chNFe = txtNovaChaveNFe.Text;
            lstOutrasInf.Items.Add("-> " + nfe.NFeChaveNota.InformacoesNFe.chNFe);

            string nmArquivoCaminho = openFileXml.FileName;
            int pos = (nmArquivoCaminho.IndexOf("NFe") == -1 ? nmArquivoCaminho.IndexOf("NFE") : 0);
            //string nmCaminho = nmArquivoCaminho.Substring(0, nmArquivoCaminho.IndexOf("NFe"));
            string nmCaminho = nmArquivoCaminho.Substring(0, pos);
            //string nmArquivo = nmArquivoCaminho.Substring(nmArquivoCaminho.IndexOf("NFe"));
            string nmArquivo = nmArquivoCaminho.Substring(pos);
            lstOutrasInf.Items.Add("-> " + nmArquivoCaminho);
            nmArquivo = nmArquivo.Replace(txtChave.Text, txtNovaChaveNFe.Text);
            nmArquivoCaminho = nmCaminho + nmArquivo;
            lstOutrasInf.Items.Add("-> " + nmArquivoCaminho);
            /* Populando os produtos */
            int i = 0;

            lstVwProdutos.Items.Clear();
            foreach (var item in nfe.NotaFiscalEletronica.InformacoesNFe.Detalhe)
            {
                i++;
                ListViewItem oItem = new ListViewItem(item.nItem.ToString());
                oItem.SubItems.Add(item.Produto.cProd);
                oItem.SubItems.Add(item.Produto.cEAN);
                oItem.SubItems.Add(item.Produto.xProd);
                oItem.SubItems.Add(item.Produto.NCM);
                oItem.SubItems.Add(item.Produto.CFOP);
                oItem.SubItems.Add(item.Produto.uCom);
                oItem.SubItems.Add(item.Produto.qCom.ToString());
                oItem.SubItems.Add(item.Produto.vUnCom.ToString());
                oItem.SubItems.Add(item.Produto.vUnTrib.ToString());
                oItem.SubItems.Add(item.Produto.vProd.ToString());
                oItem.SubItems.Add(item.Produto.xPed);
                oItem.SubItems.Add(item.infAdProd2);
                lstVwProdutos.Items.Add(oItem);
            }



        }

        private string GeraNovaChave(string chaveAtual, string dataEmiNFe, string nrNovaNFe, string serieNFe)
        {
            string novaChave = chaveAtual.Substring(0, 2);
            novaChave = novaChave + dataEmiNFe.Substring(8, 2);
            novaChave = novaChave + dataEmiNFe.Substring(3, 2);
            novaChave = novaChave + chaveAtual.Substring(6,16);
            novaChave = novaChave + serieNFe.PadLeft(3, '0');
            novaChave = novaChave + nrNovaNFe.PadLeft(9,'0');
            novaChave = novaChave + chaveAtual.Substring(34, 9);
            int soma = 0;
            int peso = 4;
            int digito = 0;
            for (int i = 0; i < novaChave.Length; i++)
            {
                soma += int.Parse(novaChave[i].ToString()) * peso;
                peso = ((peso - 1) == 1 ? 9 : peso-=1);
                
            }
            digito = (((soma % 11) == 1 || (soma % 11) == 0) ? 0 : (11 - (soma % 11)));
            novaChave = novaChave + digito.ToString();
            return novaChave;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            foreach (var item in nfe.NotaFiscalEletronica.InformacoesNFe.Detalhe)
            {

            }
        }
    }

}
