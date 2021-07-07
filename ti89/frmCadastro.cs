using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ti89
{
    public partial class frmCadastro : Form
    {
        public frmCadastro()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente(txtNome.Text, txtEmail.Text);
            cliente.Inserir();
            MessageBox.Show(cliente.mensagem,"Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtId.Text = Convert.ToString(cliente.ID);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            if (txtId.Text==string.Empty)
            {
                MessageBox.Show("Informar o ID de busca!","Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Focus();
                return;
            }
            else
            {
                cliente.Consultar(Convert.ToInt32(txtId.Text));

            }

            if (cliente.achou==true)
            {
                txtNome.Text = cliente.Nome;
                txtEmail.Text = cliente.Email;
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
            }
            else
            {
                MessageBox.Show(cliente.mensagem,"Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = false;
                Limpar();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente(Convert.ToInt32(txtId.Text), txtNome.Text, txtEmail.Text);
            cliente.Alterar();
            MessageBox.Show(cliente.mensagem,"Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmCadastro_Load(object sender, EventArgs e)
        {
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.Deletar(Convert.ToInt32(txtId.Text));
            MessageBox.Show(cliente.mensagem, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Limpar();
        }

        public void Limpar()
        {
            txtId.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }
    }
}
