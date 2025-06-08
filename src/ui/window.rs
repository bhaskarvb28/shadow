use crate::browser::engine::run_url_parser;
use crate::ui::panels;
use eframe::egui;

pub struct GuiApp {
    pub url_input: String,
    pub output: String,
}

impl Default for GuiApp {
    fn default() -> Self {
        Self {
            url_input: String::new(),
            output: String::new(),
        }
    }
}

impl eframe::App for GuiApp {
    fn update(&mut self, ctx: &egui::Context, _frame: &mut eframe::Frame) {
        egui::CentralPanel::default().show(ctx, |ui| {
            panels::url_input_panel(ui, &mut self.url_input, &mut self.output, run_url_parser);
        });
    }
}
