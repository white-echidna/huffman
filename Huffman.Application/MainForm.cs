namespace Huffman.Application;

public enum AppState
{
    Idle,
    Processing
}

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        SetState(AppState.Idle);
    }

    private void SetState(AppState state)
    {
        switch (state)
        {
            case AppState.Idle:
                btnCompress.Enabled = true;
                btnDecompress.Enabled = true;
                progressBar.Visible = false;
                lblStatus.Text = "Ready";
                Cursor = Cursors.Default;
                break;

            case AppState.Processing:
                btnCompress.Enabled = false;
                btnDecompress.Enabled = false;
                progressBar.Visible = true;
                lblStatus.Text = "Processing...";
                Cursor = Cursors.WaitCursor;
                break;
        }
    }

    private async void BtnCompress_Click(object sender, EventArgs e)
    {
        using var openDialog = new OpenFileDialog
        {
            Title = "Choose file to compress",
            Filter = "All files (*.*)|*.*"
        };

        if (openDialog.ShowDialog() != DialogResult.OK) return;
        string sourcePath = openDialog.FileName;

        using var saveDialog = new SaveFileDialog
        {
            Title = "Save archive as",
            Filter = "Huffman Archive (*.huf)|*.huf",
            FileName = Path.GetFileName(sourcePath) + ".huf"
        };

        if (saveDialog.ShowDialog() != DialogResult.OK) return;
        string destPath = saveDialog.FileName;

        try
        {
            SetState(AppState.Processing);

            await Task.Run(() => RunCompression(sourcePath, destPath));

            MessageBox.Show(
                "Operation complete!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Operation failed:\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        finally
        {
            SetState(AppState.Idle);
        }
    }

    private async void BtnDecompress_Click(object sender, EventArgs e)
    {
        using var openDialog = new OpenFileDialog
        {
            Title = "Choose archive to unpack",
            Filter = "Huffman Archive (*.huf)|*.huf"
        };

        if (openDialog.ShowDialog() != DialogResult.OK) return;
        string sourcePath = openDialog.FileName;

        using var saveDialog = new SaveFileDialog
        {
            Title = "Save target file",
            Filter = "All files (*.*)|*.*",
            FileName = Path.GetFileNameWithoutExtension(sourcePath)
        };

        if (saveDialog.ShowDialog() != DialogResult.OK) return;
        string destPath = saveDialog.FileName;

        try
        {
            SetState(AppState.Processing);
            await Task.Run(() => RunDecompression(sourcePath, destPath));
            MessageBox.Show(
                "Operation complete!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Operation failure:\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        finally
        {
            SetState(AppState.Idle);
        }
    }

    private void RunCompression(string inputFile, string outputFile)
    {
        Thread.Sleep(1000);
    }

    private void RunDecompression(string inputFile, string outputFile)
    {
        Thread.Sleep(1000);
    }
}
