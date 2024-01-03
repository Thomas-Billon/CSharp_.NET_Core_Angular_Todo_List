const PROXY_CONFIG = [
  {
    context: [
      "/task"
    ],
    target: "https://localhost:7029",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
