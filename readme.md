# UI Automation COM problem

Repro:

Works with WPF interfaces:

- Make sure a UI automation client is running, i.e.:
  - C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64\inspect.exe
  - Accessibility Insights for Windows https://accessibilityinsights.io/
- Make sure the `WPF` conditional compilation symbol is defined
- Run the program
- `WindowProvider.Navigate` will be called (which throws to make sure you notice)

Doesn't work when using interface defined in assembly:

- Remove the `WPF` conditional compilation symbol
- Run the program
- `WindowProvider.Navigate` will be called (which throws to make sure you notice)
