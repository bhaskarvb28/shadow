mod networking;

use networking::url::ParsedUrl;

fn main() {
    // let url = "https://example.com:443/path/page.html?foo=bar#anchor";
    let url = "https://www.google.com";
    let parsed = ParsedUrl::parse(url).expect("Invalid URL");

    println!("{:#?}", parsed);
}
