mod browser;
mod networking;
mod ui;

fn main() -> eframe::Result<()> {
    let options = eframe::NativeOptions::default();
    eframe::run_native(
        "Rust Browser - URL Parser",
        options,
        Box::new(|_cc| Box::new(ui::window::GuiApp::default())),
    )
}
