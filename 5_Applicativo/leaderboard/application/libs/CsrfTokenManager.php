<?php
namespace libs;
class CsrfTokenManager
{
    private string $sessionKey;

    public function __construct(string $sessionKey = 'csrf_token') {
        if (session_status() === PHP_SESSION_NONE) {
            session_start();
        }
        $this->sessionKey = $sessionKey;
        $this->initializeToken();
    }

    private function initializeToken(): void {
        if (!isset($_SESSION[$this->sessionKey])) {
            $_SESSION[$this->sessionKey] = bin2hex(random_bytes(32));
        }

    }

    public static function validateToken(string $token): bool {
        return hash_equals($_SESSION['csrf_token'] ?? '', $token);  //evita timing attack
    }
}