import { Layout } from "antd";
import { Menu } from "antd";
import { Header } from "antd/es/layout/layout";
import Link from "next/link";
import Content from "antd/es/layout/layout"
import Flooter from "antd/es/layout/layout"
import "./globals.css";
import { Color } from "antd/es/color-picker";

const items = [
  {key: "home", label: <Link href={"/"}>Home</Link>},
  {key: "pech", label: <Link href={"/pech"}>Pech</Link>},
  {key: "diagram", label: <Link href={"/diagram"}>Diagram</Link>},
]

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Layout style={{minHeight: "100vh"}}>
          <Header>
            <Menu theme="dark" mode="horizontal" items={items} style={{flex: 1, minWidth: 0}}>
            </Menu>
          </Header>
          <Content style={{padding: "0 48px"}}>{children}</Content>
          <Flooter style={{ textAlign: "center" }}>
            Pech created Fedorovtsev Danil 2024
          </Flooter>
        </Layout>
      </body>
    </html>
  );
}
