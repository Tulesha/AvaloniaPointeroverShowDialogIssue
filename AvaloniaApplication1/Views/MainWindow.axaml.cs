using Avalonia.Controls;
using Avalonia.Input;

namespace AvaloniaApplication1.Views;

public interface ILinkInfo
{
    void Navigate(PointerPressedEventArgs e);
}

public class LinkInfoDialog : ILinkInfo
{
    private readonly Window _ownerWindow;

    public LinkInfoDialog(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
    }

    public async void Navigate(PointerPressedEventArgs e)
    {
        if (e.KeyModifiers == KeyModifiers.Control)
        {
            // Show ContextMenu
            var contextMenu = new ContextMenu
            {
                Items = { new MenuItem { Header = "Menu1" }, new MenuItem { Header = "Menu2" } }
            };
            contextMenu.Open(e.Source as Control);
            return;
        }

        // Some kind of dialog
        await new Window().ShowDialog(_ownerWindow);
    }
}

public class LinkInfoSmth : ILinkInfo
{
    private readonly Window _ownerWindow;

    public LinkInfoSmth(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
    }

    public void Navigate(PointerPressedEventArgs e)
    {
        // Do smth in non async context
    }
}

public partial class MainWindow : Window
{
    private readonly ILinkInfo _linkInfo1;
    private readonly ILinkInfo _linkInfo2;

    public MainWindow()
    {
        InitializeComponent();

        _linkInfo1 = new LinkInfoDialog(this);
        _linkInfo2 = new LinkInfoSmth(this);
    }

    private void Link_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            return;

        if (Equals(sender, Link1))
            _linkInfo1.Navigate(e);
        else
            _linkInfo2.Navigate(e);

        e.Handled = true;
    }
}