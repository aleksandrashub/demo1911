using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demo1111.Models;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace demo1111;

public partial class AddEditProd : Window
{
    public string path;
    public string destPath;
    public Bitmap bitmap;
    public string filename;
    public Prod product = null;

    public List<string> edizmBind = Helper.mycontext.EdIzms.Select(x => x.EdIzm1).ToList();
    public List<string> categ = Helper.mycontext.Categories.Select(x => x.Categ).ToList();
    public List<string> manuf = Helper.mycontext.Manufacturers.Select(x => x.Manuf).ToList();
    public List<string> custs = Helper.mycontext.Customers.Select(x => x.Customer1).ToList();
    public AddEditProd()
    {
        InitializeComponent();
        edizmCmb.ItemsSource = edizmBind;
    }
    public AddEditProd(Prod prod)
    {
        InitializeComponent();
        edizmCmb.ItemsSource = edizmBind;
        imgOut.Source = prod.bitmap;
        NameTxt.Text = prod.NameProd;
        descrTxt.Text = prod.Descr;
        product = prod;
    }

    private async void img_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        var res = await openFileDialog.ShowAsync(this);
        if (res == null) return;
        path = string.Join("", res);
        if (res != null)
        {
            bitmap = new Bitmap(path);
        }
        imgOut.Source = bitmap;
        filename = System.IO.Path.GetFileName(path);
        destPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Assets";
        

    }

    private void Ok_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        bool check = true;
        switch (product)
        {
            case null:
                product.IdProd = Helper.mycontext.Prods.OrderBy(x => x.IdProd).LastOrDefault().IdProd;
                product.IdProd += 1;
                if (filename != null)
                {
                    product.Image = filename;
                }
                else
                {
                    product.Image = "picture_zagl.png";
                }
                break;
            case object:
                
                if (filename != null)
                {
                    product.Image = filename;
                }
                else
                {
                    product.Image = "picture_zagl.png";
                }


                break;
        }
       
        if (NameTxt.Text.Length > 0)
        {
            product.NameProd = NameTxt.Text;
        }
        else
        {
            check = false;

            var msg = MessageBoxManager.GetMessageBoxStandard("������", "���� ������������ ����������� ��� ����������");
            msg.ShowAsync();
        }
        if (manufCmb.SelectedIndex != -1)
        {
            product.IdManufNavigation = Helper.mycontext.Manufacturers.Where(x => x.Manuf == manufCmb.SelectedValue.ToString()).FirstOrDefault();
            product.IdManuf = Helper.mycontext.Manufacturers.Where(x => x.Manuf == manufCmb.SelectedValue.ToString()).FirstOrDefault().IdManuf;


        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("������", "�������� �������������");
            msg.ShowAsync();
        }
        if (custCmb.SelectedIndex != -1)
            if (custCmb.SelectedIndex != -1)
            {
                product.IdCustNavigation = Helper.mycontext.Customers.Where(x => x.Customer1 == custCmb.SelectedValue.ToString()).FirstOrDefault();
                product.IdCust = Helper.mycontext.Customers.Where(x => x.Customer1 == custCmb.SelectedValue.ToString()).FirstOrDefault().IdCustomer;


            }
            else
            {
                check = false;
                var msg = MessageBoxManager.GetMessageBoxStandard("������", "�������� ����������");
                msg.ShowAsync();
            }
        if (categCmb.SelectedIndex != -1)
            if (categCmb.SelectedIndex != -1)
            {
                product.IdCategNavigation = Helper.mycontext.Categories.Where(x => x.Categ == categCmb.SelectedValue.ToString()).FirstOrDefault();
                product.IdCateg = Helper.mycontext.Categories.Where(x => x.Categ == categCmb.SelectedValue.ToString()).FirstOrDefault().IdCateg;


            }
            else
            {
                check = false;
                var msg = MessageBoxManager.GetMessageBoxStandard("������", "�������� ���������");
                msg.ShowAsync();
            }
        int quan;
        if (quanTxt.Text.Length > 0 && Int32.TryParse(quanTxt.Text, out quan))
        {
            product.QuanSkl = quan;
        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("������", "��������� ���� ���������� �� ������");
            msg.ShowAsync();
        }
        float maxD;
        if (maxdiscTxt.Text.Length > 0 && float.TryParse(maxdiscTxt.Text, out maxD))
        {
            product.MaxDisc = maxD;
        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("������", "��������� ���� ������������ ������");
            msg.ShowAsync();
        }
        float currD;
        if (currdiscTxt.Text.Length > 0 && float.TryParse(currdiscTxt.Text, out currD))
        {
            product.CurrDisc = currD;
        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("������", "��������� ���� ������� ������");
            msg.ShowAsync();

        }
        int cost;
        if (costTxt.Text.Length > 0 && Int32.TryParse(costTxt.Text, out cost))
        {
            product.Cost = cost;
        }
        else
        {
            var msg = MessageBoxManager.GetMessageBoxStandard("������", "��������� ���� ���������");
            msg.ShowAsync();
            check = false;
        }
        if (descrTxt.Text.Length > 0)
        {
            product.Descr = descrTxt.Text;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("������", "��������� ���� ��������");
            msg.ShowAsync();
        }
        if (articulTxt.Text.Length > 0)
        {
            product.Articul = articulTxt.Text;
        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("������", "��������� ���� ��������");
            msg.ShowAsync();
        }


        if (check)
        {

            Helper.mycontext.Prods.Add(product);
            Helper.mycontext.SaveChanges();
        
        
        
        }




    }

    
}


