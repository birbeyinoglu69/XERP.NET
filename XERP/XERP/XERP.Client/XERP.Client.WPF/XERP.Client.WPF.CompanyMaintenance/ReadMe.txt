WPF Sample for Simple Mvvm Toolkit

This demonstrates how to use the Simple Mvvm Toolkit to create a WPF MVVM app.

The setup is basically the same as for the Siverlight Getting Started app,
the main difference being that you need to reference SimpleMvvmToolkit-WPF.

Add refences to System.Windows.Interactivity and Microsoft.Expression.Interactions
They can be found here:
C:\Program Files (x86)\Microsoft SDKs\Expression\Blend\.NETFramework\v4.0\Libraries

Don't forget to reference the namespaces in XAML, like so:

xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
xmlns:ei="http://schemas.microsoft.com/expression/2010/interactions"

VERSION 2 UPDATE: Two changes were required for version 2 of the Simple MVVM Toolkit.
1) Removed and re-added ViewModelLocator using the new locator item template
   - Used mvvmlocator and mvvmlocatornosa code snippets to add ViewModels
2) Removed square brackets in DataContext for ProductListView and ProductDetailView
   - This references a property in the locator rather than an indexer

Then you can insert event triggers like so:

<i:Interaction.Triggers>
    <i:EventTrigger EventName="Click">
        <ei:CallMethodAction 
                TargetObject="{Binding}"
                MethodName="NewCustomer"/>
    </i:EventTrigger>
</i:Interaction.Triggers>

