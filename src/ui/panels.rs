use eframe::egui::{TextEdit, TextStyle, Ui};

pub fn url_input_panel<F>(ui: &mut Ui, url_input: &mut String, output: &mut String, parse_fn: F)
where
    F: Fn(&str) -> Result<String, String>,
{
    ui.heading("URL Parser");

    ui.horizontal(|ui| {
        ui.label("Enter URL:");
        ui.text_edit_singleline(url_input);
    });

    if ui.button("Parse").clicked() {
        *output = match parse_fn(url_input) {
            Ok(result) => result,
            Err(e) => e,
        };
    }

    ui.separator();
    ui.label("Parsed Output:");

    ui.add(
        TextEdit::multiline(output)
            .desired_rows(10)
            .font(TextStyle::Monospace),
    );
}
