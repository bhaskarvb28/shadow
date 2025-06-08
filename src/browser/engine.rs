use crate::networking::url::ParsedUrl;

pub fn run_url_parser(url: &str) -> Result<String, String> {
    ParsedUrl::parse(url)
        .map(|parsed| format!("{:#?}", parsed))
        .map_err(|e| format!("Parse error: {:?}", e))
}
