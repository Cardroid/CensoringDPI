using System;
using System.Collections.Generic;
using System.Text;

namespace GoodByeDPI.NET.Manual
{
    public class ArgumentManual
    {
        private static IReadOnlyDictionary<string, string> ArgsManual;

        public static IReadOnlyDictionary<string, string> GetArgumentManual()
        {
            if (ArgsManual == null)
            {
                Dictionary<string, string> argsManual = new Dictionary<string, string>();

                argsManual.Add("-p", "패시브 DPI 차단");
                argsManual.Add("-r", "호스트를 [hoSt]로 교체");
                argsManual.Add("-s", "host 헤더의 빈칸 제거");
                argsManual.Add("-m", "Host 헤더를 섞기(test.com->tEsT.cOm)");
                argsManual.Add("-f [value]", "설정값에 맞춰서 HTTP 패킷을 쪼갭니다. 작은 값을 수록 더 많이 쪼개집니다.");
                argsManual.Add("-k [value]", "HTTP 지속성(keep-alive)패킷을 쪼갭니다. 작은 값을 수록 더 많이 쪼개집니다.");
                argsManual.Add("-n", "-k가 활성화된 경우 첫 번째 세그먼트 ACK 대기 안 함");
                argsManual.Add("-e [value]", "설정값에 맞춰서 HTTPS 패킷을 쪼갭니다. 작은 값을 수록 더 많이 쪼개집니다.");
                argsManual.Add("-a", "Method와 Request-URI 사이의 추가 공간 (-s 사용 가능, 사이트를 손상시킬 수 있음)");
                argsManual.Add("-w", "모든 처리된 포트에서 HTTP 트래픽을 찾아 구문 분석 (포트 80에만 해당하지 않음)");
                argsManual.Add("--port [value]", "조각화를 수행할 추가 TCP 포트 (-w 를 사용한 HTTP 트릭)");
                argsManual.Add("--ip-id [value]", "추가 IP ID 처리 (십진수, 이 ID를 사용하여 리디렉션 및 TCP RST를 Drop하십시오) (이 옵션은 여러 번 사용할 수 있음)");
                argsManual.Add("--dns-addr [value]", "제공된 IP 주소로 UDP DNS 요청 리디렉션 (실험적 기능)");
                argsManual.Add("--dns-port [value]", "제공된 포트로 UDP DNS 요청 재연결 (기본값은 53번)");
                argsManual.Add("--dnsv6-addr [value]", "제공된 IPv6 주소로 UDPv6 DNS 요청 리디렉션 (실험적 기능)");
                argsManual.Add("--dnsv6-port [value]", "제공된 포트로 UDPv6 DNS 요청 재연결 (기본값은 53번)");
                argsManual.Add("--dns-verb", "자세한 DNS 리디렉션 메시지 인쇄");
                argsManual.Add("--blacklist [txtfile]", "호스트 이름 및 하위 도메인에 대해서만 HTTP 트릭 수행, 텍스트 파일로 입력가능 (이 옵션은 여러 번 사용할 수 있음)");
                argsManual.Add("--set-ttl [value]", "Fake Request Mode를 활성화하고 제공된 TTL[value]으로 보냅니다. [위험! 예기치 않은 방식으로 웹 사이트를 손상시킬 수 있습니다. 주의해서 사용하십시오]");
                argsManual.Add("--wrong-chksum", "Fake Request Mode를 활성화하고 잘못된 TCP 체크섬과 함께 출력, VM 또는 일부 라우터에서는 작동하지 않을 수 있지만 --set-ttl보다 안전합니다.");
                argsManual.Add("-1", "(-p -r -s -f 2 -k 2 -n -e 2) 가장 호환성 높은 방식, 가장 느림 (기본값)");
                argsManual.Add("-2", "(-p -r -s -f 2 -k 2 -n -e 40) HTTPS의 속도가 더 빠르지만 여전히 호환성 높음");
                argsManual.Add("-3", "(-p -r -s -e 40) HTTPS 속도가 더 빠르며, HTTP 파편화를 진행하지 않음");
                argsManual.Add("-4", "(-p -r -s) 가장 빠름");

                ArgsManual = argsManual;
            }

            return ArgsManual;
        }
    }
}
