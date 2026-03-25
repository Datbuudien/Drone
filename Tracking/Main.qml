import QtQuick
import QtQuick.Window
import DroneApp 1.0
Window {
    width: 1080
    height: 720
    visible: true
    title: qsTr("Drone Ground Control")
    UdpReceiver{
        id : receiver
    }
    Text{
        anchors.centerIn: parent
        text: "Coordinate:\n\n" + receiver.coordinate
        color: "#00ffcc"
        font.pixelSize: 32
        font.bold: true
        horizontalAlignment: Text.AlignHCenter
    }
}
