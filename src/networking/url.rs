use url::Url;

#[derive(Debug)]
pub struct ParsedUrl {
    pub scheme: String,
    pub host: String,
    pub port: u16,
    pub path: String,
    pub query: Option<String>,
}

impl ParsedUrl {
    pub fn parse(input: &str) -> Result<Self, String> {
        let parsed = Url::parse(input).map_err(|e| format!("URL parse error: {e}"))?;

        let scheme = parsed.scheme().to_string();
        let host = parsed.host_str().ok_or("No host found")?.to_string();
        let port = parsed.port_or_known_default().ok_or("No port found")?;
        let path = parsed.path().to_string();
        let query = parsed.query().map(|q| q.to_string());

        Ok(ParsedUrl {
            scheme,
            host,
            port,
            path,
            query,
        })
    }
}
